using Microsoft.AspNetCore.Mvc;

using SpaceX.Core.Services.Interfaces;
using SpaceX.WebApi.Contracts.Requests;
using SpaceX.WebApi.Contracts.Responses;
using SpaceX.WebApi.Extensions;
using SpaceX.WebApi.Filters;
using SpaceX.WebApi.Mappings;

namespace SpaceX.WebApi.Controllers;

[ApiController]
[AuthRequired]
[Route("launch")]
public sealed class LaunchController : ControllerBase
{
    private readonly ILaunchService _launchService;

    public LaunchController(ILaunchService launchService)
    {
        _launchService = launchService;
    }

    /// <summary>
    /// Get latest launch
    /// </summary>
    [HttpGet("latest")]
    [ProducesResponseType(typeof(ApiResponse<LaunchResponse?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLatestLaunch(CancellationToken cancellationToken)
    {
        var response = await _launchService.GetLatestLaunchAsync(cancellationToken);

        return this.SuccessResponse(response?.ToContract());
    }

    /// <summary>
    /// Get upcoming/past launches
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PaginatedLaunchesResponse?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLaunches([FromQuery] GetLaunchesRequest request, CancellationToken cancellationToken)
    {
        var response = await _launchService.GetLaunchesAsync(request.ToDomain(), cancellationToken);

        return this.SuccessResponse(response?.ToContract());
    }
}