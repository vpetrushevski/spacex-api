namespace SpaceX.Core.Domain.Models.Responses;

public class PaginatedLaunchesResponse
{
    public required IReadOnlyList<LaunchResponse> Docs { get; init; }

    public int TotalDocs { get; init; }

    public int Limit { get; init; }

    public int TotalPages { get; init; }

    public int Page { get; init; }

    public int PagingCounter { get; init; }

    public bool HasPrevPage { get; init; }

    public bool HasNextPage { get; init; }

    public int? PrevPage { get; init; }

    public int? NextPage { get; init; }
}

