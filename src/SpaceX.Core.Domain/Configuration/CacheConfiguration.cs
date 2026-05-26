namespace SpaceX.Core.Domain.Configuration;

public class CacheConfiguration
{
    public required string LatestLaunchCacheKey { get; init; }

    public int LatestLaunchExpirationInMinutes { get; init; }

    public required string LaunchesCacheKey { get; init; }

    public int LaunchesExpirationInMinutes { get; init; }
}

