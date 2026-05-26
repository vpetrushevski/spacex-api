using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;

public sealed class RedditResponse
{
    [JsonPropertyName("campaign")]
    public string? Campaign { get; set; }

    [JsonPropertyName("launch")]
    public string? Launch { get; set; }

    [JsonPropertyName("media")]
    public string? Media { get; set; }

    [JsonPropertyName("recovery")]
    public string? Recovery { get; set; }
}