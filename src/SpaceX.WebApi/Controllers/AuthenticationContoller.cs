using Microsoft.AspNetCore.Mvc;

using SpaceX.Core.Services.Interfaces;
using SpaceX.Core.Services.Interfaces.Authentication;
using SpaceX.WebApi.Contracts.Requests;
using SpaceX.WebApi.Contracts.Responses;
using SpaceX.WebApi.Extensions;
using SpaceX.WebApi.Filters;
using SpaceX.WebApi.Mappings;

namespace SpaceX.WebApi.Controllers;

[ApiController]
[Route("authentication")]
public sealed class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IAccountService _accountService;

    public AuthenticationController(
        IAuthenticationService authenticationService,
        IAccountService accountService)
    {
        _authenticationService = authenticationService;
        _accountService = accountService;
    }

    /// <summary>
    /// Login
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var response = await _authenticationService.LoginAsync(request.ToDomain(), cancellationToken);

        return this.SuccessResponse(response);
    }

    /// <summary>
    /// Authorize
    /// </summary>
    [HttpGet("authorize")]
    [AuthRequired]
    [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Authorize(CancellationToken cancellationToken)
    {
        var accessToken = Request.Headers.Authorization.ToString();

        var response = await _authenticationService.AuthorizeAsync(accessToken, cancellationToken);

        return this.SuccessResponse(response);
    }

    /// <summary>
    /// Refresh token
    /// </summary>
    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshTokens([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var response = await _authenticationService.RefreshTokenAsync(request.ToDomain(), cancellationToken);

        return this.SuccessResponse(response);
    }

    /// <summary>
    /// Logout
    /// </summary>
    [HttpPost("logout")]
    [AuthRequired]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest request, CancellationToken cancellationToken)
    {
        await _authenticationService.LogoutAsync(request.ToDomain(), cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Verify account
    /// </summary>
    [HttpPost("verify")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> VerifyAccount([FromBody] VerifyAccountRequest request, CancellationToken cancellationToken)
    {
        await _authenticationService.VerifyAccountAsync(request.ToDomain(), cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Send verification email
    /// </summary>
    [HttpPost("{email}/resend-verification-email")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> SendVerificationEmail([FromRoute] string email, CancellationToken cancellationToken)
    {
        await _authenticationService.SendVerificationEmailAsync(email, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Send forgot password email
    /// </summary>
    [HttpPost("{email}/forgot-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> SendForgotPasswordEmail([FromRoute] string email, CancellationToken cancellationToken)
    {
        await _authenticationService.SendForgotPasswordEmailAsync(email, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Resend forgot password email
    /// </summary>
    [HttpPost("{email}/resend-forgot-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ResendForgotPasswordEmail([FromRoute] string email, CancellationToken cancellationToken)
    {
        await _authenticationService.SendForgotPasswordEmailAsync(email, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Reset password
    /// </summary>
    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        await _authenticationService.ResetPasswordAsync(request.ToDomain(), cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Change password
    /// </summary>
    [HttpPost("change-password")]
    [AuthRequired]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        await _authenticationService.ChangePasswordAsync(request.ToDomain(), cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Check is email registered
    /// </summary>
    [HttpGet("check-email/{email}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CheckIsEmailRegistered([FromRoute] string email, CancellationToken cancellationToken)
    {
        var response = await _accountService.CheckIsEmailRegisteredAsync(email, cancellationToken);

        return this.SuccessResponse(response);
    }
}