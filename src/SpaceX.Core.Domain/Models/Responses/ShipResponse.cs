namespace SpaceX.Core.Domain.Models.Responses;

public class ShipResponse
{
    public string? LegacyId { get; init; }

    public string? Model { get; init; }

    public required string Type { get; init; }

    public required IReadOnlyList<string> Roles { get; init; }

    public int? YearBuilt { get; init; }

    public required string HomePort { get; init; }

    public string? Status { get; init; }

    public double? Latitude { get; init; }

    public double? Longitude { get; init; }

    public string? Link { get; init; }

    public string? Image { get; init; }

    public required string Name { get; init; }

    public bool Active { get; init; }

    public required string Id { get; init; }
}