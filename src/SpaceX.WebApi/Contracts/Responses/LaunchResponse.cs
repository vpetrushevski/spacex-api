namespace SpaceX.WebApi.Contracts.Responses;

public sealed record LaunchResponse
{
    public required LinksResponse Links { get; init; }

    public required string Rocket { get; set; }

    public bool? Success { get; init; }

    public string? Details { get; init; }

    public required IReadOnlyList<CrewResponse> Crew { get; init; }

    public required IReadOnlyList<string> Ships { get; init; }

    public required IReadOnlyList<string> Capsules { get; init; }

    public required string Launchpad { get; init; }

    public int FlightNumber { get; init; }

    public required string Name { get; init; }

    public DateTimeOffset DateUtc { get; init; }

    public bool Upcoming { get; init; }

    public required IReadOnlyList<CoreResponse> Cores { get; init; }

    public required string Id { get; init; }
}

