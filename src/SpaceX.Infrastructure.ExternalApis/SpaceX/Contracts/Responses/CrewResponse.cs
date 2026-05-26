using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;

public sealed class CrewResponse
{
    [JsonPropertyName("crew")]
    public required string Crew { get; set; }

    [JsonPropertyName("role")]
    public required string Role { get; set; }
}