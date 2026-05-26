using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Requests;

public sealed class GetLaunchesRequest
{
    [JsonPropertyName("query")]
    public required LaunchQueryRequest Query { get; set; }

    [JsonPropertyName("options")]
    public required LaunchOptionsRequest Options { get; set; }
}