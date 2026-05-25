using System.Security.Claims;

using SpaceX.Core.Domain.Models.Requests;
using SpaceX.Core.Domain.Models.Responses;

namespace SpaceX.Core.Services.Interfaces.Authentication;

public interface IAuthenticationService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);

    Task<LoginResponse> AuthorizeAsync(string accessToken);

    Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest request);

    ClaimsIdentity ValidateAccessToken(string accessToken, bool validateExpirationTime = true);

    Task LogoutAsync(LogoutRequest request);

    Task SendVerificationEmailAsync(string email);

    Task VerifyAccountAsync(VerifyAccountRequest request);

    Task SendForgotPasswordEmailAsync(string email);

    Task ResetPasswordAsync(ResetPasswordRequest request);

    Task ChangePasswordAsync(ChangePasswordRequest request);
}

