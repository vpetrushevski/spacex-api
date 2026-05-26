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
            _memoryCache.Set(_cacheConfiguration.LatestLaunchCacheKey, response, TimeSpan.FromMinutes(_cacheConfiguration.LatestLaunchExpirationInMinutes));
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
            _memoryCache.Set(cacheKey, response, TimeSpan.FromMinutes(_cacheConfiguration.LatestLaunchExpirationInMinutes));
        }

        return response;
    }

    public async Task<LaunchResponse?> GetLaunchDetailsAsync(string launchId, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"{_cacheConfiguration.LaunchDetailsCacheKey}:launch:{launchId}";

        if (_memoryCache.TryGetValue(cacheKey, out LaunchResponse? cachedLaunch))
        {
            return cachedLaunch;
        }

        var response = await _spaceXApiClient.GetLaunchDetailsAsync(launchId, cancellationToken);

        if (response is not null)
        {
            _memoryCache.Set(cacheKey, response, TimeSpan.FromHours(_cacheConfiguration.LaunchDetailsExpirationInHours));
        }

        return response;
    }

    public async Task<RocketResponse?> GetRocketDetailsAsync(string rocketId, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"{_cacheConfiguration.LaunchDetailsCacheKey}:rocket:{rocketId}";

        if (_memoryCache.TryGetValue(cacheKey, out RocketResponse? cachedRocket))
        {
            return cachedRocket;
        }

        var response = await _spaceXApiClient.GetRocketDetailsAsync(rocketId, cancellationToken);

        if (response is not null)
        {
            _memoryCache.Set(cacheKey, response, TimeSpan.FromHours(_cacheConfiguration.LaunchDetailsExpirationInHours));
        }

        return response;
    }

    public async Task<LaunchpadResponse?> GetLaunchpadDetailsAsync(string launchpadId, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"{_cacheConfiguration.LaunchDetailsCacheKey}:launchpad:{launchpadId}";

        if (_memoryCache.TryGetValue(cacheKey, out LaunchpadResponse? cachedLaunchpad))
        {
            return cachedLaunchpad;
        }

        var response = await _spaceXApiClient.GetLaunchpadDetailsAsync(launchpadId, cancellationToken);

        if (response is not null)
        {
            _memoryCache.Set(cacheKey, response, TimeSpan.FromHours(_cacheConfiguration.LaunchDetailsExpirationInHours));
        }

        return response;
    }

    public async Task<LandpadResponse?> GetLandpadDetailsAsync(string landpadId, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"{_cacheConfiguration.LaunchDetailsCacheKey}:landpad:{landpadId}";

        if (_memoryCache.TryGetValue(cacheKey, out LandpadResponse? cachedLandpad))
        {
            return cachedLandpad;
        }

        var response = await _spaceXApiClient.GetLandpadDetailsAsync(landpadId, cancellationToken);

        if (response is not null)
        {
            _memoryCache.Set(cacheKey, response, TimeSpan.FromHours(_cacheConfiguration.LaunchDetailsExpirationInHours));
        }

        return response;
    }

    public async Task<CrewMemberResponse?> GetCrewMemberDetailsAsync(string crewMemberId, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"{_cacheConfiguration.LaunchDetailsCacheKey}:crew:{crewMemberId}";

        if (_memoryCache.TryGetValue(cacheKey, out CrewMemberResponse? cachedCrewMember))
        {
            return cachedCrewMember;
        }

        var response = await _spaceXApiClient.GetCrewMemberDetailsAsync(crewMemberId, cancellationToken);

        if (response is not null)
        {
            _memoryCache.Set(cacheKey, response, TimeSpan.FromHours(_cacheConfiguration.LaunchDetailsExpirationInHours));
        }

        return response;
    }

    public async Task<CapsuleResponse?> GetCapsuleDetailsAsync(string capsuleId, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"{_cacheConfiguration.LaunchDetailsCacheKey}:capsule:{capsuleId}";

        if (_memoryCache.TryGetValue(cacheKey, out CapsuleResponse? cachedCapsule))
        {
            return cachedCapsule;
        }

        var response = await _spaceXApiClient.GetCapsuleDetailsAsync(capsuleId, cancellationToken);

        if (response is not null)
        {
            _memoryCache.Set(cacheKey, response, TimeSpan.FromHours(_cacheConfiguration.LaunchDetailsExpirationInHours));
        }

        return response;
    }

    public async Task<ShipResponse?> GetShipDetailsAsync(string shipId, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"{_cacheConfiguration.LaunchDetailsCacheKey}:ship:{shipId}";

        if (_memoryCache.TryGetValue(cacheKey, out ShipResponse? cachedShip))
        {
            return cachedShip;
        }

        var response = await _spaceXApiClient.GetShipDetailsAsync(shipId, cancellationToken);

        if (response is not null)
        {
            _memoryCache.Set(cacheKey, response, TimeSpan.FromHours(_cacheConfiguration.LaunchDetailsExpirationInHours));
        }

        return response;
    }

    private string BuildLaunchesCacheKey(GetLaunchesRequest request)
    {
        return $"{_cacheConfiguration.LaunchesCacheKey}:upcoming:{request.Upcoming}:page:{request.Page}:limit:{request.Limit}:sort:{request.SortDirection}";
    }
}

