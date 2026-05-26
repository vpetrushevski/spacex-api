using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;

public sealed class RocketDimensionResponse
{
    [JsonPropertyName("meters")]
    public double? Meters { get; set; }

    [JsonPropertyName("feet")]
    public double? Feet { get; set; }
}