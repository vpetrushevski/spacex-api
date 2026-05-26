using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;

public sealed class FailureResponse
{
    [JsonPropertyName("time")]
    public int? Time { get; set; }

    [JsonPropertyName("altitude")]
    public int? Altitude { get; set; }

    [JsonPropertyName("reason")]
    public string? Reason { get; set; }
}