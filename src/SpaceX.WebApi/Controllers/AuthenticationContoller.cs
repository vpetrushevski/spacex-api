using Microsoft.AspNetCore.Mvc;

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

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _authenticationService.LoginAsync(request.ToDomain());

        return this.SuccessResponse(response);
    }

    [HttpGet("authorize")]
    [AuthRequired]
    [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Authorize()
    {
        var accessToken = Request.Headers.Authorization.ToString();

        var response = await _authenticationService.AuthorizeAsync(accessToken);

        return this.SuccessResponse(response);
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> RefreshTokens([FromBody] RefreshTokenRequest request)
    {
        var response = await _authenticationService.RefreshTokenAsync(request.ToDomain());

        return this.SuccessResponse(response);
    }

    [HttpPost("logout")]
    [AuthRequired]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
    {
        await _authenticationService.LogoutAsync(request.ToDomain());

        return NoContent();
    }

    [HttpPost("verify")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> VerifyAccount([FromBody] VerifyAccountRequest request)
    {
        await _authenticationService.VerifyAccountAsync(request.ToDomain());

        return NoContent();
    }

    [HttpPost("{email}/resend-verification-email")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> SendVerificationEmail([FromRoute] string email)
    {
        await _authenticationService.SendVerificationEmailAsync(email);

        return NoContent();
    }

    [HttpPost("{email}/forgot-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> SendForgotPasswordEmail([FromRoute] string email)
    {
        await _authenticationService.SendForgotPasswordEmailAsync(email);

        return NoContent();
    }

    [HttpPost("{email}/resend-forgot-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ResendForgotPasswordEmail([FromRoute] string email)
    {
        await _authenticationService.SendForgotPasswordEmailAsync(email);

        return NoContent();
    }

    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        await _authenticationService.ResetPasswordAsync(request.ToDomain());

        return NoContent();
    }

    [HttpPost("change-password")]
    [AuthRequired]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        await _authenticationService.ChangePasswordAsync(request.ToDomain());

        return NoContent();
    }
}

