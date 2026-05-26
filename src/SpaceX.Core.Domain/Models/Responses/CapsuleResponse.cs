namespace SpaceX.Core.Domain.Models.Responses;

public class CapsuleResponse
{
    public int ReuseCount { get; init; }

    public int WaterLandings { get; init; }

    public int LandLandings { get; init; }

    public string? LastUpdate { get; init; }

    public required string Serial { get; init; }

    public required string Status { get; init; }

    public required string Type { get; init; }

    public required string Id { get; init; }
}