namespace SpaceX.Core.Domain.Models.Requests;

public class GetLaunchesRequest
{
    public bool Upcoming { get; init; }

    public int Page { get; init; }

    public int Limit { get; init; }

    public string SortDirection { get; init; } = "desc";
}