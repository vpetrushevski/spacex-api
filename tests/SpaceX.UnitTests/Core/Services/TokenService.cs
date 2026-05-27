using System.IdentityModel.Tokens.Jwt;

using Microsoft.Extensions.Options;

using Moq;

using SpaceX.Core.Domain.Configuration;
using SpaceX.Core.Domain.Entities;
using SpaceX.Core.Domain.Entities.Enums;
using SpaceX.Core.Services.Authentication;
using SpaceX.Core.Services.Helpers;
using SpaceX.Infrastructure.Interfaces.Database.Repositories;

namespace SpaceX.UnitTests.Core.Services;

public class TokenServiceTests
{
    private readonly TokenService _sut;

    private readonly Mock<IAuthenticationRepository> _authenticationRepositoryMock = new();

    public TokenServiceTests()
    {
        _sut = new TokenService(
            _authenticationRepositoryMock.Object,
            CreateEncryptionHelper(),
            Options.Create(CreateJwtTokenConfiguration()));
    }

    [Fact]
    public void Constructor_WhenJwtTokenConfigurationIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IOptions<JwtTokenConfiguration>? jwtTokenConfiguration = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(
            () => new TokenService(
                _authenticationRepositoryMock.Object,
                CreateEncryptionHelper(),
                jwtTokenConfiguration!));

        // Assert
        Assert.Equal("jwtTokenConfiguration", exception.ParamName);
    }

    [Fact]
    public async Task GenerateTokens_WhenAccountIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        Account? account = null;

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            () => _sut.GenerateTokens(account!, CancellationToken.None));

        // Assert
        Assert.Equal("account", exception.ParamName);
    }

    [Fact]
    public async Task GenerateTokens_WhenAccountIsValid_ReturnsLoginResponse()
    {
        // Arrange
        var account = CreateAccount();

        _authenticationRepositoryMock
            .Setup(x => x.CreateRefreshTokenAsync(
                It.IsAny<RefreshToken>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _sut.GenerateTokens(account, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.False(string.IsNullOrWhiteSpace(result.AccessToken));
        Assert.False(string.IsNullOrWhiteSpace(result.RefreshToken));

        Assert.NotNull(result.Account);
        Assert.Equal(account.Id, result.Account.Id);
        Assert.Equal(account.FirstName, result.Account.FirstName);
        Assert.Equal(account.LastName, result.Account.LastName);
        Assert.Equal("test@test.com", result.Account.Email);

        _authenticationRepositoryMock.Verify(
            x => x.CreateRefreshTokenAsync(
                It.Is<RefreshToken>(refreshToken =>
                    refreshToken.AccountId == account.Id &&
                    !string.IsNullOrWhiteSpace(refreshToken.Token) &&
                    refreshToken.ExpiresAtUtc > DateTimeOffset.UtcNow),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GenerateTokens_WhenAccountIsValid_CreatesAccessTokenWithExpectedClaims()
    {
        // Arrange
        var account = CreateAccount();
        var jwtTokenConfiguration = CreateJwtTokenConfiguration();

        _authenticationRepositoryMock
            .Setup(x => x.CreateRefreshTokenAsync(
                It.IsAny<RefreshToken>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _sut.GenerateTokens(account, CancellationToken.None);

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.ReadJwtToken(result.AccessToken);

        // Assert
        // Assert
        Assert.Equal(jwtTokenConfiguration.ValidIssuer, token.Issuer);
        Assert.Contains(jwtTokenConfiguration.ValidAudience, token.Audiences);

        Assert.Equal(account.Id.ToString(), token.Claims.First(x => x.Type == "nameid").Value);
        Assert.Equal(account.FirstName, token.Claims.First(x => x.Type == "given_name").Value);
        Assert.Equal(account.LastName, token.Claims.First(x => x.Type == "family_name").Value);
        Assert.Equal("test@test.com", token.Claims.First(x => x.Type == "email").Value);
    }

    [Fact]
    public async Task GenerateTokens_WhenAccountIsValid_CreatesHashedRefreshToken()
    {
        // Arrange
        var account = CreateAccount();

        RefreshToken? createdRefreshToken = null;

        _authenticationRepositoryMock
            .Setup(x => x.CreateRefreshTokenAsync(
                It.IsAny<RefreshToken>(),
                It.IsAny<CancellationToken>()))
            .Callback<RefreshToken, CancellationToken>((refreshToken, _) =>
            {
                createdRefreshToken = refreshToken;
            })
            .Returns(Task.CompletedTask);

        // Act
        var result = await _sut.GenerateTokens(account, CancellationToken.None);

        // Assert
        Assert.NotNull(createdRefreshToken);
        Assert.Equal(account.Id, createdRefreshToken.AccountId);
        Assert.False(string.IsNullOrWhiteSpace(createdRefreshToken.Token));
        Assert.NotEqual(result.RefreshToken, createdRefreshToken.Token);
        Assert.Equal(SecurityHelper.HashString(result.RefreshToken), createdRefreshToken.Token);
        Assert.True(createdRefreshToken.ExpiresAtUtc > DateTimeOffset.UtcNow);
    }

    private static Account CreateAccount()
    {
        return new Account
        {
            Id = Guid.NewGuid(),
            FirstName = "Vlatko",
            LastName = "Petrushevski",
            Email = CreateEncryptionHelper().Encrypt("test@test.com"),
            Password = BCrypt.Net.BCrypt.HashPassword("Password123!"),
            Status = AccountStatus.Active,
            IsVerified = true,
            VerificationToken = null,
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