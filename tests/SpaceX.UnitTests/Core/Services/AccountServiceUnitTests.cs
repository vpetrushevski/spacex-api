using System.ComponentModel.DataAnnotations;

using Microsoft.Extensions.Options;

using Moq;

using SpaceX.Core.Domain.Configuration;
using SpaceX.Core.Domain.Entities;
using SpaceX.Core.Domain.Entities.Enums;
using SpaceX.Core.Domain.Models.Email;
using SpaceX.Core.Domain.Models.Requests;
using SpaceX.Core.Services.Accounts;
using SpaceX.Core.Services.Helpers;
using SpaceX.Infrastructure.Interfaces.Database.Repositories;
using SpaceX.Infrastructure.Interfaces.Email;

namespace SpaceX.UnitTests.Core.Services;

public class AccountServiceTests
{
    private readonly AccountService _sut;

    private readonly Mock<IAccountRepository> _accountRepositoryMock = new();
    private readonly Mock<IEmailBackgroundDispatcher> _emailBackgroundDispatcherMock = new();

    public AccountServiceTests()
    {
        _sut = new AccountService(
            _accountRepositoryMock.Object,
            _emailBackgroundDispatcherMock.Object,
            CreateEncryptionHelper());
    }

    [Fact]
    public async Task CreateAccountAsync_WhenRequestIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        CreateAccountRequest? request = null;

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            () => _sut.CreateAccountAsync(request!, CancellationToken.None));

        // Assert
        Assert.Equal("request", exception.ParamName);
    }

    [Fact]
    public async Task CreateAccountAsync_WhenEmailAlreadyExists_ThrowsValidationException()
    {
        // Arrange
        var request = CreateCreateAccountRequest();

        _accountRepositoryMock
            .Setup(x => x.GetAccountByEmailAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateAccount());

        // Act
        var exception = await Assert.ThrowsAsync<ValidationException>(
            () => _sut.CreateAccountAsync(request, CancellationToken.None));

        // Assert
        Assert.Equal("Email is already registered to other account.", exception.Message);

        _accountRepositoryMock.Verify(
            x => x.GetAccountByEmailAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _accountRepositoryMock.Verify(
            x => x.CreateAccountAsync(
                It.IsAny<Account>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        _emailBackgroundDispatcherMock.Verify(
            x => x.EnqueueAsync(
                It.IsAny<EmailMessage>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task CreateAccountAsync_WhenRequestIsValid_CreatesAccountAndEnqueuesVerificationEmail()
    {
        // Arrange
        var request = CreateCreateAccountRequest();

        _accountRepositoryMock
            .Setup(x => x.GetAccountByEmailAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Account?)null);

        _accountRepositoryMock
            .Setup(x => x.CreateAccountAsync(
                It.IsAny<Account>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _emailBackgroundDispatcherMock
            .Setup(x => x.EnqueueAsync(
                It.IsAny<EmailMessage>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _sut.CreateAccountAsync(request, CancellationToken.None);

        // Assert
        _accountRepositoryMock.Verify(
            x => x.GetAccountByEmailAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _accountRepositoryMock.Verify(
            x => x.CreateAccountAsync(
                It.Is<Account>(account =>
                    account.Id != Guid.Empty &&
                    account.FirstName == "Vlatko" &&
                    account.LastName == "Petrushevski" &&
                    account.Email != request.Email &&
                    account.Password != request.Password &&
                    account.Status == AccountStatus.AwaitingConfirmation &&
                    account.IsVerified == false &&
                    !string.IsNullOrWhiteSpace(account.VerificationToken)),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _emailBackgroundDispatcherMock.Verify(
            x => x.EnqueueAsync(
                It.Is<EmailMessage>(message =>
                    message.Type == EmailType.Verification &&
                    message.Email == "test@test.com" &&
                    message.FirstName == "Vlatko" &&
                    message.LastName == "Petrushevski" &&
                    message.AccountId.HasValue &&
                    !string.IsNullOrWhiteSpace(message.Token)),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task CheckIsEmailRegisteredAsync_WhenEmailIsNullOrWhiteSpace_ThrowsArgumentException(string? email)
    {
        // Arrange

        // Act
        var exception = await Assert.ThrowsAnyAsync<ArgumentException>(
            () => _sut.CheckIsEmailRegisteredAsync(email!, CancellationToken.None));

        // Assert
        Assert.Equal("email", exception.ParamName);
    }

    [Fact]
    public async Task CheckIsEmailRegisteredAsync_WhenAccountExists_ReturnsTrue()
    {
        // Arrange
        const string email = "test@test.com";

        _accountRepositoryMock
            .Setup(x => x.GetAccountByEmailAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateAccount());

        // Act
        var result = await _sut.CheckIsEmailRegisteredAsync(email, CancellationToken.None);

        // Assert
        Assert.True(result);

        _accountRepositoryMock.Verify(
            x => x.GetAccountByEmailAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckIsEmailRegisteredAsync_WhenAccountDoesNotExist_ReturnsFalse()
    {
        // Arrange
        const string email = "test@test.com";

        _accountRepositoryMock
            .Setup(x => x.GetAccountByEmailAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Account?)null);

        // Act
        var result = await _sut.CheckIsEmailRegisteredAsync(email, CancellationToken.None);

        // Assert
        Assert.False(result);

        _accountRepositoryMock.Verify(
            x => x.GetAccountByEmailAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    private static CreateAccountRequest CreateCreateAccountRequest()
    {
        return new CreateAccountRequest
        {
            FirstName = "Vlatko",
            LastName = "Petrushevski",
            Email = "test@test.com",
            Password = "Password123!"
        };
    }

    private static Account CreateAccount()
    {
        return new Account
        {
            Id = Guid.NewGuid(),
            FirstName = "Vlatko",
            LastName = "Petrushevski",
            Email = "encrypted-email",
            Password = "hashed-password",
            Status = AccountStatus.Active,
            IsVerified = true,
            VerificationToken = "verification-token",
            CreatedAtUtc = DateTimeOffset.UtcNow
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
}