using System.Security.Claims;

using SpaceX.Core.Domain.Models.Requests;
using SpaceX.Core.Domain.Models.Responses;

namespace SpaceX.Core.Services.Interfaces.Authentication;

public interface IAuthenticationService
{
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);

    Task<LoginResponse> AuthorizeAsync(string accessToken, CancellationToken cancellationToken = default);

    Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);

    ClaimsIdentity ValidateAccessToken(string accessToken, bool validateExpirationTime = true);

    Task LogoutAsync(LogoutRequest request, CancellationToken cancellationToken = default);

    Task SendVerificationEmailAsync(string email, CancellationToken cancellationToken = default);

    Task VerifyAccountAsync(VerifyAccountRequest request, CancellationToken cancellationToken = default);

    Task SendForgotPasswordEmailAsync(string email, CancellationToken cancellationToken = default);

    Task ResetPasswordAsync(ResetPasswordRequest request, CancellationToken cancellationToken = default);

    Task ChangePasswordAsync(ChangePasswordRequest request, CancellationToken cancellationToken = default);
}

