using SpaceX.Core.Domain.Models.Requests;
using SpaceX.Core.Domain.Models.Responses;

namespace SpaceX.Infrastructure.Interfaces.ExternalApis.SpaceX;

public interface ISpaceXApiClient
{
    Task<LaunchResponse?> GetLatestLaunchAsync(CancellationToken cancellationToken = default);

    Task<PaginatedLaunchesResponse?> GetLaunchesAsync(GetLaunchesRequest request, CancellationToken cancellationToken = default);

    Task<LaunchResponse?> GetLaunchDetailsAsync(string launchId, CancellationToken cancellationToken = default);

    Task<RocketResponse?> GetRocketDetailsAsync(string rocketId, CancellationToken cancellationToken = default);

    Task<LaunchpadResponse?> GetLaunchpadDetailsAsync(string launchpadId, CancellationToken cancellationToken = default);

    Task<LandpadResponse?> GetLandpadDetailsAsync(string landpadId, CancellationToken cancellationToken = default);

    Task<CrewMemberResponse?> GetCrewMemberDetailsAsync(string crewMemberId, CancellationToken cancellationToken = default);

    Task<CapsuleResponse?> GetCapsuleDetailsAsync(string capsuleId, CancellationToken cancellationToken = default);

    Task<ShipResponse?> GetShipDetailsAsync(string shipId, CancellationToken cancellationToken = default);
}

