namespace SpaceX.Core.Domain.Models.Responses;

public class LinksResponse
{
    public required PatchResponse Patch { get; init; }

    public required RedditResponse Reddit { get; init; }

    public required FlickrResponse Flickr { get; init; }

    public string? Presskit { get; init; }

    public string? Webcast { get; init; }

    public string? YoutubeId { get; init; }

    public string? Article { get; init; }

    public string? Wikipedia { get; init; }
}

