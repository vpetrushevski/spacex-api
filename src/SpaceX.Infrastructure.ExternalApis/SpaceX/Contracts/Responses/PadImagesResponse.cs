using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;

public sealed class PadImagesResponse
{
    [JsonPropertyName("large")]
    public required IReadOnlyList<string> Large { get; set; }
}