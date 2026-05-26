using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;

public sealed class ShipResponse
{
    [JsonPropertyName("legacy_id")]
    public string? LegacyId { get; set; }

    [JsonPropertyName("model")]
    public string? Model { get; set; }

    [JsonPropertyName("type")]
    public required string Type { get; set; }

    [JsonPropertyName("roles")]
    public required IReadOnlyList<string> Roles { get; set; }

    [JsonPropertyName("year_built")]
    public int? YearBuilt { get; set; }

    [JsonPropertyName("home_port")]
    public required string HomePort { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("latitude")]
    public double? Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double? Longitude { get; set; }

    [JsonPropertyName("link")]
    public string? Link { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }
}