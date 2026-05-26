using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;

public sealed class FlickrResponse
{
    [JsonPropertyName("small")]
    public required IReadOnlyList<string> Small { get; set; }

    [JsonPropertyName("original")]
    public required IReadOnlyList<string> Original { get; set; }
}