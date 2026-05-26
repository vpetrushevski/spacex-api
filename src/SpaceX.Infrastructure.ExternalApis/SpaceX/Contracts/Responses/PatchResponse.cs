using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;

public sealed class PatchResponse
{
    [JsonPropertyName("small")]
    public string? Small { get; set; }

    [JsonPropertyName("large")]
    public string? Large { get; set; }
}