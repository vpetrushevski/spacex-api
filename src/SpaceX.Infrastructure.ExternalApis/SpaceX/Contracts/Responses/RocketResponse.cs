using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;

public sealed class RocketResponse
{
    [JsonPropertyName("height")]
    public required RocketDimensionResponse Height { get; set; }

    [JsonPropertyName("diameter")]
    public required RocketDimensionResponse Diameter { get; set; }

    [JsonPropertyName("mass")]
    public required RocketMassResponse Mass { get; set; }

    [JsonPropertyName("flickr_images")]
    public required IReadOnlyList<string> FlickrImages { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("type")]
    public required string Type { get; set; }

    [JsonPropertyName("active")]
    public bool Active { get; set; }

    [JsonPropertyName("stages")]
    public int Stages { get; set; }

    [JsonPropertyName("boosters")]
    public int Boosters { get; set; }

    [JsonPropertyName("cost_per_launch")]
    public long CostPerLaunch { get; set; }

    [JsonPropertyName("success_rate_pct")]
    public int SuccessRatePct { get; set; }

    [JsonPropertyName("first_flight")]
    public required string FirstFlight { get; set; }

    [JsonPropertyName("country")]
    public required string Country { get; set; }

    [JsonPropertyName("company")]
    public required string Company { get; set; }

    [JsonPropertyName("wikipedia")]
    public required string Wikipedia { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }
}

