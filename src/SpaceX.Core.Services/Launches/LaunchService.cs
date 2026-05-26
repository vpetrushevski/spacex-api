using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

using SpaceX.Core.Domain.Configuration;
using SpaceX.Core.Domain.Models.Requests;
using SpaceX.Core.Domain.Models.Responses;
using SpaceX.Core.Services.Interfaces;
using SpaceX.Infrastructure.Interfaces.ExternalApis.SpaceX;

namespace SpaceX.Core.Services.Launches;

public class LaunchService : ILaunchService
{
    private readonly ISpaceXApiClient _spaceXApiClient;
    private readonly IMemoryCache _memoryCache;
    private readonly CacheConfiguration _cacheConfiguration;

    public LaunchService(
        ISpaceXApiClient spaceXApiClient,
        IMemoryCache memoryCache,
        IOptions<CacheConfiguration> cacheConfiguration)
    {
        ArgumentNullException.ThrowIfNull(cacheConfiguration);

        _spaceXApiClient = spaceXApiClient;
        _memoryCache = memoryCache;
        _cacheConfiguration = cacheConfiguration.Value;
    }

    public async Task<LaunchResponse?> GetLatestLaunchAsync(CancellationToken cancellationToken = default)
    {
        if (_memoryCache.TryGetValue(_cacheConfiguration.LatestLaunchCacheKey, out LaunchResponse? cachedLaunch))
        {
            return cachedLaunch;
        }

        var response = await _spaceXApiClient.GetLatestLaunchAsync(cancellationToken);

        if (response is not null)
        {
            _memoryCache.Set(_cacheConfiguration.LatestLaunchCacheKey, response, TimeSpan.FromMinutes(10));
        }

        return response;
    }

    public async Task<PaginatedLaunchesResponse?> GetLaunchesAsync(GetLaunchesRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var cacheKey = BuildLaunchesCacheKey(request);

        if (_memoryCache.TryGetValue(cacheKey, out PaginatedLaunchesResponse? cachedLaunches))
        {
            return cachedLaunches;
        }

        var response = await _spaceXApiClient.GetLaunchesAsync(request, cancellationToken);

        if (response is not null)
        {
            _memoryCache.Set(cacheKey, response, TimeSpan.FromMinutes(5));
        }

        return response;
    }

    private string BuildLaunchesCacheKey(GetLaunchesRequest request)
    {
        return $"{_cacheConfiguration.LaunchesCacheKey}:upcoming:{request.Upcoming}:page:{request.Page}:limit:{request.Limit}:sort:{request.SortDirection}";
    }
}

