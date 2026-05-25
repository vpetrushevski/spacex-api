using SpaceX.Core.Domain.Models.Requests;
using SpaceX.Core.Domain.Models.Responses;

namespace SpaceX.WebApi.Mappings;

public static class AuthenticationMappings
{
    public static LoginRequest ToDomain(this Contracts.Requests.LoginRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new LoginRequest
        {
            Email = request.Email,
            Password = request.Password
        };
    }

    public static Contracts.Responses.LoginResponse ToContract(this LoginResponse response)
    {
        ArgumentNullException.ThrowIfNull(response);

        return new Contracts.Responses.LoginResponse
        {
            AccessToken = response.AccessToken,
            RefreshToken = response.RefreshToken,
            Account = response.Account.ToContract()
        };
    }

    public static LogoutRequest ToDomain(this Contracts.Requests.LogoutRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new LogoutRequest
        {
            RefreshToken = request.RefreshToken
        };
    }

    public static VerifyAccountRequest ToDomain(this Contracts.Requests.VerifyAccountRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new VerifyAccountRequest
        {
            AccountId = request.AccountId,
            Token = request.Token
        };
    }

    public static RefreshTokenRequest ToDomain(this Contracts.Requests.RefreshTokenRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new RefreshTokenRequest
        {
            AccessToken = request.AccessToken,
            RefreshToken = request.RefreshToken
        };
    }

    public static ResetPasswordRequest ToDomain(this Contracts.Requests.ResetPasswordRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new ResetPasswordRequest
        {
            AccountId = request.AccountId,
            ResetPasswordToken = request.ResetPasswordToken,
            NewPassword = request.NewPassword
        };
    }

    public static ChangePasswordRequest ToDomain(this Contracts.Requests.ChangePasswordRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new ChangePasswordRequest
        {
            CurrentPassword = request.CurrentPassword,
            NewPassword = request.NewPassword
        };
    }
}