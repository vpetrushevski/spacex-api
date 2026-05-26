using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Requests;

public sealed class LaunchQueryRequest
{
    [JsonPropertyName("upcoming")]
    public bool Upcoming { get; set; }
}

