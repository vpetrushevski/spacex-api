using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;

public sealed class LaunchpadResponse : BasePadResponse
{
    [JsonPropertyName("launch_attempts")]
    public int LaunchAttempts { get; set; }

    [JsonPropertyName("launch_successes")]
    public int LaunchSuccesses { get; set; }

    [JsonPropertyName("rockets")]
    public required IReadOnlyList<string> Rockets { get; set; }

    [JsonPropertyName("timezone")]
    public required string Timezone { get; set; }
}