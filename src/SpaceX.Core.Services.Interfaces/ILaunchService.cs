using SpaceX.Core.Domain.Models.Requests;
using SpaceX.Core.Domain.Models.Responses;

namespace SpaceX.Core.Services.Interfaces;

public interface ILaunchService
{
    Task<LaunchResponse?> GetLatestLaunchAsync(CancellationToken cancellationToken = default);

    Task<PaginatedLaunchesResponse?> GetLaunchesAsync(GetLaunchesRequest request, CancellationToken cancellationToken = default);
}

