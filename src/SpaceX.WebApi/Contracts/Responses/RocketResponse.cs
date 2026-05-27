namespace SpaceX.WebApi.Contracts.Responses;

public sealed record RocketResponse
{
    public required IReadOnlyList<string> FlickrImages { get; init; }

    public required string Name { get; init; }

    public required string Type { get; init; }

    public long CostPerLaunch { get; init; }

    public int SuccessRatePct { get; init; }

    public required string Description { get; init; }

    public required string Id { get; init; }
}