using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Requests;

public sealed class LaunchSortRequest
{
    [JsonPropertyName("date_utc")]
    public required string DateUtc { get; set; }
}