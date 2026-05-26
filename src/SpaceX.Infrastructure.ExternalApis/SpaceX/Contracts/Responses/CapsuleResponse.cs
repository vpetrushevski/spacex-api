using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;

public sealed class CapsuleResponse
{
    [JsonPropertyName("reuse_count")]
    public int ReuseCount { get; set; }

    [JsonPropertyName("water_landings")]
    public int WaterLandings { get; set; }

    [JsonPropertyName("land_landings")]
    public int LandLandings { get; set; }

    [JsonPropertyName("last_update")]
    public string? LastUpdate { get; set; }

    [JsonPropertyName("serial")]
    public required string Serial { get; set; }

    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("type")]
    public required string Type { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }
}