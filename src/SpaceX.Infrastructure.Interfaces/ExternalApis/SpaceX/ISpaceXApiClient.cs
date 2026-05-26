using SpaceX.Core.Domain.Models.Requests;
using SpaceX.Core.Domain.Models.Responses;

namespace SpaceX.Infrastructure.Interfaces.ExternalApis.SpaceX;

public interface ISpaceXApiClient
{
    Task<LaunchResponse?> GetLatestLaunchAsync(CancellationToken cancellationToken = default);

    Task<PaginatedLaunchesResponse?> GetLaunchesAsync(GetLaunchesRequest request, CancellationToken cancellationToken = default);
}

