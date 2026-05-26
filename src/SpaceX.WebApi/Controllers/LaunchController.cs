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
    [HttpGet("list")]
    [ProducesResponseType(typeof(ApiResponse<PaginatedLaunchesResponse?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLaunches([FromQuery] GetLaunchesRequest request, CancellationToken cancellationToken)
    {
        var response = await _launchService.GetLaunchesAsync(request.ToDomain(), cancellationToken);

        return this.SuccessResponse(response?.ToContract());
    }

    /// <summary>
    /// Get launch details
    /// </summary>
    [HttpGet("{launchId}")]
    [ProducesResponseType(typeof(ApiResponse<LaunchResponse?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLaunchDetails(string launchId, CancellationToken cancellationToken)
    {
        var response = await _launchService.GetLaunchDetailsAsync(launchId, cancellationToken);

        return this.SuccessResponse(response?.ToContract());
    }

    /// <summary>
    /// Get rocket details
    /// </summary>
    [HttpGet("rocket/{rocketId}")]
    [ProducesResponseType(typeof(ApiResponse<RocketResponse?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRocketDetails(string rocketId, CancellationToken cancellationToken)
    {
        var response = await _launchService.GetRocketDetailsAsync(rocketId, cancellationToken);

        return this.SuccessResponse(response?.ToContract());
    }

    /// <summary>
    /// Get launchpad details
    /// </summary>
    [HttpGet("launchpad/{launchpadId}")]
    [ProducesResponseType(typeof(ApiResponse<LaunchpadResponse?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLaunchpadDetails(string launchpadId, CancellationToken cancellationToken)
    {
        var response = await _launchService.GetLaunchpadDetailsAsync(launchpadId, cancellationToken);

        return this.SuccessResponse(response?.ToContract());
    }

    /// <summary>
    /// Get landpad details
    /// </summary>
    [HttpGet("landpad/{landpadId}")]
    [ProducesResponseType(typeof(ApiResponse<LandpadResponse?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLandpadDetails(string landpadId, CancellationToken cancellationToken)
    {
        var response = await _launchService.GetLandpadDetailsAsync(landpadId, cancellationToken);

        return this.SuccessResponse(response?.ToContract());
    }

    /// <summary>
    /// Get crew member details
    /// </summary>
    [HttpGet("crew-member/{crewMemberId}")]
    [ProducesResponseType(typeof(ApiResponse<CrewMemberResponse?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCrewMemberDetails(string crewMemberId, CancellationToken cancellationToken)
    {
        var response = await _launchService.GetCrewMemberDetailsAsync(crewMemberId, cancellationToken);

        return this.SuccessResponse(response?.ToContract());
    }

    /// <summary>
    /// Get capsule details
    /// </summary>
    [HttpGet("capsule/{capsuleId}")]
    [ProducesResponseType(typeof(ApiResponse<CapsuleResponse?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCapsuleDetails(string capsuleId, CancellationToken cancellationToken)
    {
        var response = await _launchService.GetCapsuleDetailsAsync(capsuleId, cancellationToken);

        return this.SuccessResponse(response?.ToContract());
    }

    /// <summary>
    /// Get ship details
    /// </summary>
    [HttpGet("ship/{shipId}")]
    [ProducesResponseType(typeof(ApiResponse<ShipResponse?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetShipDetails(string shipId, CancellationToken cancellationToken)
    {
        var response = await _launchService.GetShipDetailsAsync(shipId, cancellationToken);

        return this.SuccessResponse(response?.ToContract());
    }
}