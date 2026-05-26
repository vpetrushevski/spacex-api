namespace SpaceX.WebApi.Contracts.Responses;

public sealed record RocketResponse
{
    public required RocketDimensionResponse Height { get; init; }

    public required RocketDimensionResponse Diameter { get; init; }

    public required RocketMassResponse Mass { get; init; }

    public required IReadOnlyList<string> FlickrImages { get; init; }

    public required string Name { get; init; }

    public required string Type { get; init; }

    public bool Active { get; init; }

    public int Stages { get; init; }

    public int Boosters { get; init; }

    public long CostPerLaunch { get; init; }

    public int SuccessRatePct { get; init; }

    public required string FirstFlight { get; init; }

    public required string Country { get; init; }

    public required string Company { get; init; }

    public required string Wikipedia { get; init; }

    public required string Description { get; init; }

    public required string Id { get; init; }
}