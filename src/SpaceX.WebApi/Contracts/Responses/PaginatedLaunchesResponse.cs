namespace SpaceX.WebApi.Contracts.Responses;

public sealed record PaginatedLaunchesResponse
{
    public required IReadOnlyList<LaunchResponse> Launches { get; init; }

    public int TotalDocs { get; init; }

    public int Limit { get; init; }

    public int TotalPages { get; init; }

    public int Page { get; init; }
}

