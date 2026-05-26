namespace SpaceX.Core.Domain.Models.Responses;

public class LaunchResponse
{
    public FairingsResponse? Fairings { get; init; }

    public required LinksResponse Links { get; init; }

    public DateTimeOffset? StaticFireDateUtc { get; init; }

    public long? StaticFireDateUnix { get; init; }

    public bool Net { get; init; }

    public int? Window { get; init; }

    public required string Rocket { get; set; }

    public bool? Success { get; init; }

    public required IReadOnlyList<FailureResponse> Failures { get; init; }

    public string? Details { get; init; }

    public required IReadOnlyList<CrewResponse> Crew { get; init; }

    public required IReadOnlyList<string> Ships { get; init; }

    public required IReadOnlyList<string> Capsules { get; init; }

    public required IReadOnlyList<string> Payloads { get; init; }

    public required string Launchpad { get; init; }

    public int FlightNumber { get; init; }

    public required string Name { get; init; }

    public DateTimeOffset DateUtc { get; init; }

    public long DateUnix { get; init; }

    public DateTimeOffset DateLocal { get; init; }

    public required string DatePrecision { get; init; }

    public bool Upcoming { get; init; }

    public required IReadOnlyList<CoreResponse> Cores { get; init; }

    public bool AutoUpdate { get; init; }

    public bool Tbd { get; init; }

    public string? LaunchLibraryId { get; init; }

    public required string Id { get; init; }
}

