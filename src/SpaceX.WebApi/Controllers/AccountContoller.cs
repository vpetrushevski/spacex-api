using Microsoft.AspNetCore.Mvc;

using SpaceX.Core.Services.Interfaces;
using SpaceX.WebApi.Contracts.Requests;
using SpaceX.WebApi.Contracts.Responses;
using SpaceX.WebApi.Extensions;
using SpaceX.WebApi.Mappings;

namespace SpaceX.WebApi.Controllers;

[ApiController]
[Route("account")]
public sealed class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    /// <summary>
    /// Create account
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request, CancellationToken cancellationToken)
    {
        await _accountService.CreateAccountAsync(request.ToDomain(), cancellationToken);

        return StatusCode(StatusCodes.Status201Created);
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
