using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;

public sealed class LandpadResponse : BasePadResponse
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }

    [JsonPropertyName("landing_attempts")]
    public int LandingAttempts { get; set; }

    [JsonPropertyName("landing_successes")]
    public int LandingSuccesses { get; set; }

    [JsonPropertyName("wikipedia")]
    public required string Wikipedia { get; set; }
}