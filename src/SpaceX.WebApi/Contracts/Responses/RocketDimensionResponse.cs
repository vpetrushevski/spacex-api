namespace SpaceX.WebApi.Contracts.Responses;

public sealed record RocketDimensionResponse
{
    public double? Meters { get; init; }

    public double? Feet { get; init; }
}