using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;

public sealed class LinksResponse
{
    [JsonPropertyName("patch")]
    public required PatchResponse Patch { get; set; }

    [JsonPropertyName("reddit")]
    public required RedditResponse Reddit { get; set; }

    [JsonPropertyName("flickr")]
    public required FlickrResponse Flickr { get; set; }

    [JsonPropertyName("presskit")]
    public string? Presskit { get; set; }

    [JsonPropertyName("webcast")]
    public string? Webcast { get; set; }

    [JsonPropertyName("youtube_id")]
    public string? YoutubeId { get; set; }

    [JsonPropertyName("article")]
    public string? Article { get; set; }

    [JsonPropertyName("wikipedia")]
    public string? Wikipedia { get; set; }
}