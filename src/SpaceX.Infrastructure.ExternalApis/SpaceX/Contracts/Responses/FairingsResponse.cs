using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;

public sealed class FairingsResponse
{
    [JsonPropertyName("reused")]
    public bool? Reused { get; set; }

    [JsonPropertyName("recovery_attempt")]
    public bool? RecoveryAttempt { get; set; }

    [JsonPropertyName("recovered")]
    public bool? Recovered { get; set; }

    [JsonPropertyName("ships")]
    public required IReadOnlyList<string> Ships { get; set; }
}
