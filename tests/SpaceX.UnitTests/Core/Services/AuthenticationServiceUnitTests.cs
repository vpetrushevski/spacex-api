using System.ComponentModel.DataAnnotations;

using Microsoft.Extensions.Options;

using Moq;

using SpaceX.Core.Domain.Configuration;
using SpaceX.Core.Domain.Entities;
using SpaceX.Core.Domain.Entities.Enums;
using SpaceX.Core.Domain.Models.Authentication;
using SpaceX.Core.Domain.Models.Email;
using SpaceX.Core.Domain.Models.Requests;
using SpaceX.Core.Domain.Models.Responses;
using SpaceX.Core.Services.Authentication;
using SpaceX.Core.Services.Helpers;
using SpaceX.Core.Services.Interfaces.Authentication;
using SpaceX.Infrastructure.Interfaces.Database.Repositories;
using SpaceX.Infrastructure.Interfaces.Email;

namespace SpaceX.UnitTests.Core.Services;

public class AuthenticationServiceTests
{
    private readonly AuthenticationService _sut;

    private readonly Mock<IAccountRepository> _accountRepositoryMock = new();
    private readonly Mock<IAuthenticationRepository> _authenticationRepositoryMock = new();
    private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
    private readonly Mock<ITokenService> _tokenServiceMock = new();
    private readonly Mock<IEmailBackgroundDispatcher> _emailBackgroundDispatcherMock = new();

    public AuthenticationServiceTests()
    {
        _sut = new AuthenticationService(
            _accountRepositoryMock.Object,
            _authenticationRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _tokenServiceMock.Object,
            _emailBackgroundDispatcherMock.Object,
            CreateEncryptionHelper(),
            Options.Create(CreateJwtTokenConfiguration()));
    }

    [Fact]
    public async Task LoginAsync_WhenRequestIsValid_ReturnsLoginResponse()
    {
        // Arrange
        var request = CreateLoginRequest();
        var account = CreateAccount();
        var response = CreateLoginResponse(account);

        _accountRepositoryMock
            .Setup(x => x.GetAccountByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(account);

        _tokenServiceMock
            .Setup(x => x.GenerateTokens(account, It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _sut.LoginAsync(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.AccessToken, result.AccessToken);
        Assert.Equal(response.RefreshToken, result.RefreshToken);

        _tokenServiceMock.Verify(
            x => x.GenerateTokens(account, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task LoginAsync_WhenPasswordIsWrong_ThrowsValidationException()
    {
        // Arrange
        var request = CreateLoginRequest();
        var account = CreateAccount();

        account.Password = BCrypt.Net.BCrypt.HashPassword("WrongPassword123!");

        _accountRepositoryMock
            .Setup(x => x.GetAccountByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(account);

        // Act
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _sut.LoginAsync(request, CancellationToken.None));

        // Assert
        Assert.Equal("Wrong password.", exception.Message);
    }

    [Fact]
    public async Task LogoutAsync_WhenRefreshTokenExists_DeletesRefreshTokens()
    {
        // Arrange
        var account = CreateAccount();
        var request = CreateLogoutRequest();
        var refreshToken = CreateRefreshToken(account, request.RefreshToken);

        _currentUserServiceMock
            .Setup(x => x.GetCurrentUser())
            .Returns(new AuthenticatedUser
            {
                AccountId = account.Id,
                FirstName = "Vlatko",
                LastName = "Petrushevski",
                Email = "test@test.com"
            });

        _authenticationRepositoryMock
            .Setup(x => x.GetRefreshTokenAsync(account.Id, It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(refreshToken);

        _authenticationRepositoryMock
            .Setup(x => x.DeleteRefreshTokensByAccountIdAsync(account.Id, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _sut.LogoutAsync(request, CancellationToken.None);

        // Assert
        _authenticationRepositoryMock.Verify(
            x => x.DeleteRefreshTokensByAccountIdAsync(account.Id, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task SendVerificationEmailAsync_WhenAccountIsAwaitingConfirmation_EnqueuesVerificationEmail()
    {
        // Arrange
        const string email = "test@test.com";
        var account = CreateAccount(AccountStatus.AwaitingConfirmation);

        _accountRepositoryMock
            .Setup(x => x.GetAccountByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(account);

        _emailBackgroundDispatcherMock
            .Setup(x => x.EnqueueAsync(It.IsAny<EmailMessage>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _sut.SendVerificationEmailAsync(email, CancellationToken.None);

        // Assert
        _emailBackgroundDispatcherMock.Verify(
            x => x.EnqueueAsync(
                It.Is<EmailMessage>(message =>
                    message.Type == EmailType.Verification &&
                    message.Email == "test@test.com" &&
                    message.FirstName == account.FirstName &&
                    message.LastName == account.LastName &&
                    message.AccountId == account.Id),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task VerifyAccountAsync_WhenTokenIsValid_UpdatesAccount()
    {
        // Arrange
        var request = CreateVerifyAccountRequest();
        var account = CreateAccount(AccountStatus.AwaitingConfirmation);

        account.Id = request.AccountId;
        account.VerificationToken = SecurityHelper.HashString(request.Token);

        _accountRepositoryMock
            .Setup(x => x.GetAccountAsync(request.AccountId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(account);

        _accountRepositoryMock
            .Setup(x => x.UpdateAccountAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _sut.VerifyAccountAsync(request, CancellationToken.None);

        // Assert
        _accountRepositoryMock.Verify(
            x => x.UpdateAccountAsync(
                It.Is<Account>(updatedAccount =>
                    updatedAccount.Status == AccountStatus.Active &&
                    updatedAccount.IsVerified &&
                    updatedAccount.VerificationToken == null),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task SendForgotPasswordEmailAsync_WhenAccountExists_CreatesTokenAndEnqueuesEmail()
    {
        // Arrange
        const string email = "test@test.com";
        var account = CreateAccount();

        _accountRepositoryMock
            .Setup(x => x.GetAccountByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(account);

        _authenticationRepositoryMock
            .Setup(x => x.CreatePasswordResetTokenAsync(It.IsAny<PasswordResetToken>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _emailBackgroundDispatcherMock
            .Setup(x => x.EnqueueAsync(It.IsAny<EmailMessage>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _sut.SendForgotPasswordEmailAsync(email, CancellationToken.None);

        // Assert
        _authenticationRepositoryMock.Verify(
            x => x.CreatePasswordResetTokenAsync(
                It.Is<PasswordResetToken>(token =>
                    token.AccountId == account.Id &&
                    !string.IsNullOrWhiteSpace(token.Token)),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _emailBackgroundDispatcherMock.Verify(
            x => x.EnqueueAsync(
                It.Is<EmailMessage>(message =>
                    message.Type == EmailType.ForgotPassword &&
                    message.Email == "test@test.com" &&
                    message.AccountId == account.Id),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task ResetPasswordAsync_WhenRequestIsValid_UpdatesPasswordAndEnqueuesPasswordChangedEmail()
    {
        // Arrange
        var request = CreateResetPasswordRequest();
        var account = CreateAccount();
        var passwordResetToken = CreatePasswordResetToken(account.Id, request.ResetPasswordToken);

        account.Id = request.AccountId;

        _accountRepositoryMock
            .Setup(x => x.GetAccountAsync(request.AccountId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(account);

        _authenticationRepositoryMock
            .Setup(x => x.GetPasswordResetTokenByAccountIdAndHashedTokenAsync(
                account.Id,
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(passwordResetToken);

        _authenticationRepositoryMock
            .Setup(x => x.GetPasswordResetTokensAsync(account.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<PasswordResetToken> { passwordResetToken });

        _accountRepositoryMock
            .Setup(x => x.UpdateAccountAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _authenticationRepositoryMock
            .Setup(x => x.DeletePasswordResetTokensAsync(It.IsAny<IReadOnlyList<PasswordResetToken>>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _emailBackgroundDispatcherMock
            .Setup(x => x.EnqueueAsync(It.IsAny<EmailMessage>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _sut.ResetPasswordAsync(request, CancellationToken.None);

        // Assert
        _accountRepositoryMock.Verify(
            x => x.UpdateAccountAsync(
                It.Is<Account>(updatedAccount =>
                    BCrypt.Net.BCrypt.Verify(
                        request.NewPassword,
                        updatedAccount.Password,
                        false,
                        BCrypt.Net.HashType.SHA384)),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _emailBackgroundDispatcherMock.Verify(
            x => x.EnqueueAsync(
                It.Is<EmailMessage>(message =>
                    message.Type == EmailType.PasswordChanged &&
                    message.Email == "test@test.com"),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task ChangePasswordAsync_WhenRequestIsValid_UpdatesPassword()
    {
        // Arrange
        var request = CreateChangePasswordRequest();
        var account = CreateAccount();

        _currentUserServiceMock
            .Setup(x => x.GetCurrentUser())
            .Returns(new AuthenticatedUser
            {
                AccountId = account.Id,
                FirstName = "Vlatko",
                LastName = "Petrushevski",
                Email = "test@test.com"
            });

        _accountRepositoryMock
            .Setup(x => x.GetAccountAsync(account.Id, CancellationToken.None))
            .ReturnsAsync(account);

        _accountRepositoryMock
            .Setup(x => x.UpdateAccountAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _sut.ChangePasswordAsync(request, CancellationToken.None);

        // Assert
        _accountRepositoryMock.Verify(
            x => x.UpdateAccountAsync(
                It.Is<Account>(updatedAccount =>
                    BCrypt.Net.BCrypt.Verify(
                        request.NewPassword,
                        updatedAccount.Password,
                        false,
                        BCrypt.Net.HashType.SHA384)),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    private static LoginRequest CreateLoginRequest()
    {
        return new LoginRequest
        {
            Email = "test@test.com",
            Password = "Password123!"
        };
    }

    private static LogoutRequest CreateLogoutRequest()
    {
        return new LogoutRequest
        {
            RefreshToken = "refresh-token"
        };
    }

    private static VerifyAccountRequest CreateVerifyAccountRequest()
    {
        return new VerifyAccountRequest
        {
            AccountId = Guid.NewGuid(),
            Token = "verification-token"
        };
    }

    private static ResetPasswordRequest CreateResetPasswordRequest()
    {
        return new ResetPasswordRequest
        {
            AccountId = Guid.NewGuid(),
            ResetPasswordToken = "reset-password-token",
            NewPassword = "NewPassword123!"
        };
    }

    private static ChangePasswordRequest CreateChangePasswordRequest()
    {
        return new ChangePasswordRequest
        {
            CurrentPassword = "Password123!",
            NewPassword = "NewPassword123!"
        };
    }

    private static Account CreateAccount(AccountStatus status = AccountStatus.Active)
    {
        return new Account
        {
            Id = Guid.NewGuid(),
            FirstName = "Vlatko",
            LastName = "Petrushevski",
            Email = CreateEncryptionHelper().Encrypt("test@test.com"),
            Password = BCrypt.Net.BCrypt.HashPassword("Password123!"),
            Status = status,
            IsVerified = status == AccountStatus.Active,
            VerificationToken = "verification-token",
            CreatedAtUtc = DateTimeOffset.UtcNow
        };
    }

    private static RefreshToken CreateRefreshToken(Account account, string refreshToken)
    {
        return new RefreshToken
        {
            Id = Guid.NewGuid(),
            AccountId = account.Id,
            Token = SecurityHelper.HashString(refreshToken),
            ExpiresAtUtc = DateTimeOffset.UtcNow.AddDays(7),
            Account = account
        };
    }

    private static PasswordResetToken CreatePasswordResetToken(Guid accountId, string token)
    {
        return new PasswordResetToken
        {
            Id = Guid.NewGuid(),
            AccountId = accountId,
            Token = SecurityHelper.HashString(token),
            ExpiresAtUtc = DateTimeOffset.UtcNow.AddHours(1)
        };
    }

    private static LoginResponse CreateLoginResponse(Account account)
    {
        return new LoginResponse
        {
            AccessToken = "access-token",
            RefreshToken = "refresh-token",
            Account = new AccountResponse
            {
                Id = account.Id,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = "test@test.com"
            }
        };
    }

    private static EncryptionHelper CreateEncryptionHelper()
    {
        return new EncryptionHelper(
            Options.Create(new EncryptionConfiguration
            {
                EncryptionKey = "y1wX9m2fQ7vKcP8zJ4sLrA6nT0uE5hBxD3qWcV1oNpM=",
                InitializationVector = "Q2d6fM9u6x0mL4f0l3r0Rw=="
            }));
    }

    private static JwtTokenConfiguration CreateJwtTokenConfiguration()
    {
        return new JwtTokenConfiguration
        {
            Secret = "n2r5u8x!A%D*G-KaPdSgVkYp3s6v9y$B?E(H+MbQeThWmZq4t7w!z%C*F)J@NcRf",
            TokenValidityInMinutes = 60,
            RefreshTokenValidityInDays = 7,
            ValidAudience = "http://localhost:4200",
            ValidIssuer = "http://localhost:7019"
        };
    }
}