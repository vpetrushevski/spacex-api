using SpaceX.WebApi.Mappings;

using DomainRequests = SpaceX.Core.Domain.Models.Requests;
using DomainResponses = SpaceX.Core.Domain.Models.Responses;
using ContractRequests = SpaceX.WebApi.Contracts.Requests;
using ContractResponses = SpaceX.WebApi.Contracts.Responses;

namespace SpaceX.UnitTests.WebApi.Mappings;

public class AuthenticationMappingsTests
{
    [Fact]
    public void ToDomain_WhenLoginRequestIsValid_ReturnsLoginRequest()
    {
        // Arrange
        var request = CreateContractLoginRequest();

        // Act
        var result = request.ToDomain();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DomainRequests.LoginRequest>(result);
        Assert.Equal(request.Email, result.Email);
        Assert.Equal(request.Password, result.Password);
    }

    [Fact]
    public void ToDomain_WhenLoginRequestIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractRequests.LoginRequest? request = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => request!.ToDomain());

        // Assert
        Assert.Equal("request", exception.ParamName);
    }

    [Fact]
    public void ToContract_WhenLoginResponseIsValid_ReturnsLoginResponse()
    {
        // Arrange
        var response = CreateDomainLoginResponse();

        // Act
        var result = response.ToContract();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ContractResponses.LoginResponse>(result);
        Assert.Equal(response.AccessToken, result.AccessToken);
        Assert.Equal(response.RefreshToken, result.RefreshToken);
        Assert.NotNull(result.Account);
        Assert.Equal(response.Account.Id, result.Account.Id);
        Assert.Equal(response.Account.FirstName, result.Account.FirstName);
        Assert.Equal(response.Account.LastName, result.Account.LastName);
        Assert.Equal(response.Account.Email, result.Account.Email);
    }

    [Fact]
    public void ToContract_WhenLoginResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DomainResponses.LoginResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToContract());

        // Assert
        Assert.Equal("response", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenLogoutRequestIsValid_ReturnsLogoutRequest()
    {
        // Arrange
        var request = CreateContractLogoutRequest();

        // Act
        var result = request.ToDomain();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DomainRequests.LogoutRequest>(result);
        Assert.Equal(request.RefreshToken, result.RefreshToken);
    }

    [Fact]
    public void ToDomain_WhenLogoutRequestIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractRequests.LogoutRequest? request = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => request!.ToDomain());

        // Assert
        Assert.Equal("request", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenVerifyAccountRequestIsValid_ReturnsVerifyAccountRequest()
    {
        // Arrange
        var request = CreateContractVerifyAccountRequest();

        // Act
        var result = request.ToDomain();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DomainRequests.VerifyAccountRequest>(result);
        Assert.Equal(request.AccountId, result.AccountId);
        Assert.Equal(request.Token, result.Token);
    }

    [Fact]
    public void ToDomain_WhenVerifyAccountRequestIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractRequests.VerifyAccountRequest? request = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => request!.ToDomain());

        // Assert
        Assert.Equal("request", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenRefreshTokenRequestIsValid_ReturnsRefreshTokenRequest()
    {
        // Arrange
        var request = CreateContractRefreshTokenRequest();

        // Act
        var result = request.ToDomain();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DomainRequests.RefreshTokenRequest>(result);
        Assert.Equal(request.AccessToken, result.AccessToken);
        Assert.Equal(request.RefreshToken, result.RefreshToken);
    }

    [Fact]
    public void ToDomain_WhenRefreshTokenRequestIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractRequests.RefreshTokenRequest? request = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => request!.ToDomain());

        // Assert
        Assert.Equal("request", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenResetPasswordRequestIsValid_ReturnsResetPasswordRequest()
    {
        // Arrange
        var request = CreateContractResetPasswordRequest();

        // Act
        var result = request.ToDomain();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DomainRequests.ResetPasswordRequest>(result);
        Assert.Equal(request.AccountId, result.AccountId);
        Assert.Equal(request.ResetPasswordToken, result.ResetPasswordToken);
        Assert.Equal(request.NewPassword, result.NewPassword);
    }

    [Fact]
    public void ToDomain_WhenResetPasswordRequestIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractRequests.ResetPasswordRequest? request = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => request!.ToDomain());

        // Assert
        Assert.Equal("request", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenChangePasswordRequestIsValid_ReturnsChangePasswordRequest()
    {
        // Arrange
        var request = CreateContractChangePasswordRequest();

        // Act
        var result = request.ToDomain();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DomainRequests.ChangePasswordRequest>(result);
        Assert.Equal(request.CurrentPassword, result.CurrentPassword);
        Assert.Equal(request.NewPassword, result.NewPassword);
    }

    [Fact]
    public void ToDomain_WhenChangePasswordRequestIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractRequests.ChangePasswordRequest? request = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => request!.ToDomain());

        // Assert
        Assert.Equal("request", exception.ParamName);
    }

    private static ContractRequests.LoginRequest CreateContractLoginRequest()
    {
        return new ContractRequests.LoginRequest
        {
            Email = "test@test.com",
            Password = "Password123!"
        };
    }

    private static DomainResponses.LoginResponse CreateDomainLoginResponse()
    {
        return new DomainResponses.LoginResponse
        {
            AccessToken = "access-token",
            RefreshToken = "refresh-token",
            Account = CreateDomainAccountResponse()
        };
    }

    private static DomainResponses.AccountResponse CreateDomainAccountResponse()
    {
        return new DomainResponses.AccountResponse
        {
            Id = Guid.NewGuid(),
            FirstName = "Vlatko",
            LastName = "Petrushevski",
            Email = "test@test.com"
        };
    }

    private static ContractRequests.LogoutRequest CreateContractLogoutRequest()
    {
        return new ContractRequests.LogoutRequest
        {
            RefreshToken = "refresh-token"
        };
    }

    private static ContractRequests.VerifyAccountRequest CreateContractVerifyAccountRequest()
    {
        return new ContractRequests.VerifyAccountRequest
        {
            AccountId = Guid.NewGuid(),
            Token = "verification-token"
        };
    }

    private static ContractRequests.RefreshTokenRequest CreateContractRefreshTokenRequest()
    {
        return new ContractRequests.RefreshTokenRequest
        {
            AccessToken = "access-token",
            RefreshToken = "refresh-token"
        };
    }

    private static ContractRequests.ResetPasswordRequest CreateContractResetPasswordRequest()
    {
        return new ContractRequests.ResetPasswordRequest
        {
            AccountId = Guid.NewGuid(),
            ResetPasswordToken = "reset-password-token",
            NewPassword = "NewPassword123!"
        };
    }

    private static ContractRequests.ChangePasswordRequest CreateContractChangePasswordRequest()
    {
        return new ContractRequests.ChangePasswordRequest
        {
            CurrentPassword = "OldPassword123!",
            NewPassword = "NewPassword123!"
        };
    }
}