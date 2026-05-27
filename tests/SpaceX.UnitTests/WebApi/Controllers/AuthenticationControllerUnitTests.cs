using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Moq;

using SpaceX.Core.Services.Interfaces;
using SpaceX.Core.Services.Interfaces.Authentication;
using SpaceX.WebApi.Contracts.Requests;
using SpaceX.WebApi.Controllers;

using DomainRequests = SpaceX.Core.Domain.Models.Requests;
using DomainResponses = SpaceX.Core.Domain.Models.Responses;

namespace SpaceX.UnitTests.WebApi.Controllers;

public class AuthenticationControllerTests
{
    private readonly AuthenticationController _sut;

    private readonly Mock<IAuthenticationService> _authenticationServiceMock = new();
    private readonly Mock<IAccountService> _accountServiceMock = new();

    public AuthenticationControllerTests()
    {
        _sut = new AuthenticationController(
            _authenticationServiceMock.Object,
            _accountServiceMock.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Fact]
    public async Task Login_WhenRequestIsValid_ReturnsOkResponse()
    {
        // Arrange
        var request = CreateLoginRequest();
        var response = CreateLoginResponse();

        _authenticationServiceMock
            .Setup(x => x.LoginAsync(
                It.IsAny<DomainRequests.LoginRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _sut.Login(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        var objectResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);

        _authenticationServiceMock.Verify(
            x => x.LoginAsync(
                It.IsAny<DomainRequests.LoginRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Login_WhenServiceThrowsException_ThrowsException()
    {
        // Arrange
        var request = CreateLoginRequest();

        _authenticationServiceMock
            .Setup(x => x.LoginAsync(
                It.IsAny<DomainRequests.LoginRequest>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Login failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.Login(request, CancellationToken.None));

        // Assert
        Assert.Equal("Login failed.", exception.Message);

        _authenticationServiceMock.Verify(
            x => x.LoginAsync(
                It.IsAny<DomainRequests.LoginRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Authorize_WhenTokenIsValid_ReturnsOkResponse()
    {
        // Arrange
        const string accessToken = "Bearer access-token";
        var response = CreateLoginResponse();

        _sut.Request.Headers.Authorization = accessToken;

        _authenticationServiceMock
            .Setup(x => x.AuthorizeAsync(accessToken, It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _sut.Authorize(CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        var objectResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);

        _authenticationServiceMock.Verify(
            x => x.AuthorizeAsync(accessToken, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Authorize_WhenServiceThrowsException_ThrowsException()
    {
        // Arrange
        const string accessToken = "Bearer invalid-token";

        _sut.Request.Headers.Authorization = accessToken;

        _authenticationServiceMock
            .Setup(x => x.AuthorizeAsync(accessToken, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new UnauthorizedAccessException("Authorization failed."));

        // Act
        var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => _sut.Authorize(CancellationToken.None));

        // Assert
        Assert.Equal("Authorization failed.", exception.Message);

        _authenticationServiceMock.Verify(
            x => x.AuthorizeAsync(accessToken, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task RefreshTokens_WhenRequestIsValid_ReturnsOkResponse()
    {
        // Arrange
        var request = CreateRefreshTokenRequest();
        var response = CreateLoginResponse();

        _authenticationServiceMock
            .Setup(x => x.RefreshTokenAsync(
                It.IsAny<DomainRequests.RefreshTokenRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _sut.RefreshTokens(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        var objectResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);

        _authenticationServiceMock.Verify(
            x => x.RefreshTokenAsync(
                It.IsAny<DomainRequests.RefreshTokenRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task RefreshTokens_WhenServiceThrowsException_ThrowsException()
    {
        // Arrange
        var request = CreateRefreshTokenRequest();

        _authenticationServiceMock
            .Setup(x => x.RefreshTokenAsync(
                It.IsAny<DomainRequests.RefreshTokenRequest>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Refresh token failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.RefreshTokens(request, CancellationToken.None));

        // Assert
        Assert.Equal("Refresh token failed.", exception.Message);

        _authenticationServiceMock.Verify(
            x => x.RefreshTokenAsync(
                It.IsAny<DomainRequests.RefreshTokenRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Logout_WhenRequestIsValid_ReturnsNoContent()
    {
        // Arrange
        var request = CreateLogoutRequest();

        _authenticationServiceMock
            .Setup(x => x.LogoutAsync(
                It.IsAny<DomainRequests.LogoutRequest>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _sut.Logout(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

        _authenticationServiceMock.Verify(
            x => x.LogoutAsync(
                It.IsAny<DomainRequests.LogoutRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Logout_WhenServiceThrowsException_ThrowsException()
    {
        // Arrange
        var request = CreateLogoutRequest();

        _authenticationServiceMock
            .Setup(x => x.LogoutAsync(
                It.IsAny<DomainRequests.LogoutRequest>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Logout failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.Logout(request, CancellationToken.None));

        // Assert
        Assert.Equal("Logout failed.", exception.Message);

        _authenticationServiceMock.Verify(
            x => x.LogoutAsync(
                It.IsAny<DomainRequests.LogoutRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task VerifyAccount_WhenRequestIsValid_ReturnsNoContent()
    {
        // Arrange
        var request = CreateVerifyAccountRequest();

        _authenticationServiceMock
            .Setup(x => x.VerifyAccountAsync(
                It.IsAny<DomainRequests.VerifyAccountRequest>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _sut.VerifyAccount(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

        _authenticationServiceMock.Verify(
            x => x.VerifyAccountAsync(
                It.IsAny<DomainRequests.VerifyAccountRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task VerifyAccount_WhenServiceThrowsException_ThrowsException()
    {
        // Arrange
        var request = CreateVerifyAccountRequest();

        _authenticationServiceMock
            .Setup(x => x.VerifyAccountAsync(
                It.IsAny<DomainRequests.VerifyAccountRequest>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Verify account failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.VerifyAccount(request, CancellationToken.None));

        // Assert
        Assert.Equal("Verify account failed.", exception.Message);

        _authenticationServiceMock.Verify(
            x => x.VerifyAccountAsync(
                It.IsAny<DomainRequests.VerifyAccountRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task SendVerificationEmail_WhenEmailIsValid_ReturnsNoContent()
    {
        // Arrange
        const string email = "test@test.com";

        _authenticationServiceMock
            .Setup(x => x.SendVerificationEmailAsync(email, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _sut.SendVerificationEmail(email, CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

        _authenticationServiceMock.Verify(
            x => x.SendVerificationEmailAsync(email, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task SendVerificationEmail_WhenServiceThrowsException_ThrowsException()
    {
        // Arrange
        const string email = "test@test.com";

        _authenticationServiceMock
            .Setup(x => x.SendVerificationEmailAsync(email, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Send verification email failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.SendVerificationEmail(email, CancellationToken.None));

        // Assert
        Assert.Equal("Send verification email failed.", exception.Message);

        _authenticationServiceMock.Verify(
            x => x.SendVerificationEmailAsync(email, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task SendForgotPasswordEmail_WhenEmailIsValid_ReturnsNoContent()
    {
        // Arrange
        const string email = "test@test.com";

        _authenticationServiceMock
            .Setup(x => x.SendForgotPasswordEmailAsync(email, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _sut.SendForgotPasswordEmail(email, CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

        _authenticationServiceMock.Verify(
            x => x.SendForgotPasswordEmailAsync(email, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task SendForgotPasswordEmail_WhenServiceThrowsException_ThrowsException()
    {
        // Arrange
        const string email = "test@test.com";

        _authenticationServiceMock
            .Setup(x => x.SendForgotPasswordEmailAsync(email, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Send forgot password email failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.SendForgotPasswordEmail(email, CancellationToken.None));

        // Assert
        Assert.Equal("Send forgot password email failed.", exception.Message);

        _authenticationServiceMock.Verify(
            x => x.SendForgotPasswordEmailAsync(email, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task ResendForgotPasswordEmail_WhenEmailIsValid_ReturnsNoContent()
    {
        // Arrange
        const string email = "test@test.com";

        _authenticationServiceMock
            .Setup(x => x.SendForgotPasswordEmailAsync(email, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _sut.ResendForgotPasswordEmail(email, CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

        _authenticationServiceMock.Verify(
            x => x.SendForgotPasswordEmailAsync(email, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task ResendForgotPasswordEmail_WhenServiceThrowsException_ThrowsException()
    {
        // Arrange
        const string email = "test@test.com";

        _authenticationServiceMock
            .Setup(x => x.SendForgotPasswordEmailAsync(email, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Resend forgot password email failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.ResendForgotPasswordEmail(email, CancellationToken.None));

        // Assert
        Assert.Equal("Resend forgot password email failed.", exception.Message);

        _authenticationServiceMock.Verify(
            x => x.SendForgotPasswordEmailAsync(email, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task ResetPassword_WhenRequestIsValid_ReturnsNoContent()
    {
        // Arrange
        var request = CreateResetPasswordRequest();

        _authenticationServiceMock
            .Setup(x => x.ResetPasswordAsync(
                It.IsAny<DomainRequests.ResetPasswordRequest>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _sut.ResetPassword(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

        _authenticationServiceMock.Verify(
            x => x.ResetPasswordAsync(
                It.IsAny<DomainRequests.ResetPasswordRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task ResetPassword_WhenServiceThrowsException_ThrowsException()
    {
        // Arrange
        var request = CreateResetPasswordRequest();

        _authenticationServiceMock
            .Setup(x => x.ResetPasswordAsync(
                It.IsAny<DomainRequests.ResetPasswordRequest>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Reset password failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.ResetPassword(request, CancellationToken.None));

        // Assert
        Assert.Equal("Reset password failed.", exception.Message);

        _authenticationServiceMock.Verify(
            x => x.ResetPasswordAsync(
                It.IsAny<DomainRequests.ResetPasswordRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task ChangePassword_WhenRequestIsValid_ReturnsNoContent()
    {
        // Arrange
        var request = CreateChangePasswordRequest();

        _authenticationServiceMock
            .Setup(x => x.ChangePasswordAsync(
                It.IsAny<DomainRequests.ChangePasswordRequest>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _sut.ChangePassword(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(StatusCodes.Status204NoContent, noContentResult.StatusCode);

        _authenticationServiceMock.Verify(
            x => x.ChangePasswordAsync(
                It.IsAny<DomainRequests.ChangePasswordRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task ChangePassword_WhenServiceThrowsException_ThrowsException()
    {
        // Arrange
        var request = CreateChangePasswordRequest();

        _authenticationServiceMock
            .Setup(x => x.ChangePasswordAsync(
                It.IsAny<DomainRequests.ChangePasswordRequest>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Change password failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.ChangePassword(request, CancellationToken.None));

        // Assert
        Assert.Equal("Change password failed.", exception.Message);

        _authenticationServiceMock.Verify(
            x => x.ChangePasswordAsync(
                It.IsAny<DomainRequests.ChangePasswordRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckIsEmailRegistered_WhenEmailExists_ReturnsOkResponse()
    {
        // Arrange
        const string email = "test@test.com";

        _accountServiceMock
            .Setup(x => x.CheckIsEmailRegisteredAsync(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _sut.CheckIsEmailRegistered(email, CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        var objectResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);

        _accountServiceMock.Verify(
            x => x.CheckIsEmailRegisteredAsync(email, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task CheckIsEmailRegistered_WhenServiceThrowsException_ThrowsException()
    {
        // Arrange
        const string email = "test@test.com";

        _accountServiceMock
            .Setup(x => x.CheckIsEmailRegisteredAsync(email, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Check email failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.CheckIsEmailRegistered(email, CancellationToken.None));

        // Assert
        Assert.Equal("Check email failed.", exception.Message);

        _accountServiceMock.Verify(
            x => x.CheckIsEmailRegisteredAsync(email, It.IsAny<CancellationToken>()),
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

    private static DomainResponses.LoginResponse CreateLoginResponse()
    {
        return new DomainResponses.LoginResponse
        {
            AccessToken = "access-token",
            RefreshToken = "refresh-token",
            Account = new DomainResponses.AccountResponse
            {
                Id = Guid.NewGuid(),
                FirstName = "Vlatko",
                LastName = "Petrushevski",
                Email = "test@test.com"
            }
        };
    }

    private static RefreshTokenRequest CreateRefreshTokenRequest()
    {
        return new RefreshTokenRequest
        {
            AccessToken = "access-token",
            RefreshToken = "refresh-token"
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
            ResetPasswordToken = "reset-token",
            NewPassword = "Password123!"
        };
    }

    private static ChangePasswordRequest CreateChangePasswordRequest()
    {
        return new ChangePasswordRequest
        {
            CurrentPassword = "OldPassword123!",
            NewPassword = "NewPassword123!"
        };
    }
}