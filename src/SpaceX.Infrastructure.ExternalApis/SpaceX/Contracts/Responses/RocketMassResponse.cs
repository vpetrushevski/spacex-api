using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;

public sealed class RocketMassResponse
{
    [JsonPropertyName("kg")]
    public int Kg { get; set; }

    [JsonPropertyName("lb")]
    public int Lb { get; set; }
}