namespace SpaceX.WebApi.Contracts.Responses;

public sealed record ShipResponse
{
    public required string Type { get; init; }

    public required IReadOnlyList<string> Roles { get; init; }

    public required string HomePort { get; init; }

    public string? Image { get; init; }

    public required string Name { get; init; }

    public bool Active { get; init; }

    public required string Id { get; init; }
}