using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;

public sealed class LaunchResponse
{
    [JsonPropertyName("fairings")]
    public FairingsResponse? Fairings { get; set; }

    [JsonPropertyName("links")]
    public required LinksResponse Links { get; set; }

    [JsonPropertyName("static_fire_date_utc")]
    public DateTimeOffset? StaticFireDateUtc { get; set; }

    [JsonPropertyName("static_fire_date_unix")]
    public long? StaticFireDateUnix { get; set; }

    [JsonPropertyName("net")]
    public bool Net { get; set; }

    [JsonPropertyName("window")]
    public int? Window { get; set; }

    [JsonPropertyName("rocket")]
    public required string Rocket { get; set; }

    [JsonPropertyName("success")]
    public bool? Success { get; set; }

    [JsonPropertyName("failures")]
    public required IReadOnlyList<FailureResponse> Failures { get; set; }

    [JsonPropertyName("details")]
    public string? Details { get; set; }

    [JsonPropertyName("crew")]
    public required IReadOnlyList<CrewResponse> Crew { get; set; }

    [JsonPropertyName("ships")]
    public required IReadOnlyList<string> Ships { get; set; }

    [JsonPropertyName("capsules")]
    public required IReadOnlyList<string> Capsules { get; set; }

    [JsonPropertyName("payloads")]
    public required IReadOnlyList<string> Payloads { get; set; }

    [JsonPropertyName("launchpad")]
    public required string Launchpad { get; set; }

    [JsonPropertyName("flight_number")]
    public int FlightNumber { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("date_utc")]
    public DateTimeOffset DateUtc { get; set; }

    [JsonPropertyName("date_unix")]
    public long DateUnix { get; set; }

    [JsonPropertyName("date_local")]
    public DateTimeOffset DateLocal { get; set; }

    [JsonPropertyName("date_precision")]
    public required string DatePrecision { get; set; }

    [JsonPropertyName("upcoming")]
    public bool Upcoming { get; set; }

    [JsonPropertyName("cores")]
    public required IReadOnlyList<CoreResponse> Cores { get; set; }

    [JsonPropertyName("auto_update")]
    public bool AutoUpdate { get; set; }

    [JsonPropertyName("tbd")]
    public bool Tbd { get; set; }

    [JsonPropertyName("launch_library_id")]
    public string? LaunchLibraryId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

