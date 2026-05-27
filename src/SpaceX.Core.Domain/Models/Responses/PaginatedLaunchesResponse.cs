namespace SpaceX.Core.Domain.Models.Responses;

public class PaginatedLaunchesResponse
{
    public required IReadOnlyList<LaunchResponse> Launches { get; init; }

    public int TotalLaunches { get; init; }

    public int Limit { get; init; }

    public int TotalPages { get; init; }

    public int Page { get; init; }
}

