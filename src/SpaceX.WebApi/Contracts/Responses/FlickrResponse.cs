namespace SpaceX.WebApi.Contracts.Responses;

public sealed record FlickrResponse
{
    public required IReadOnlyList<string> Small { get; init; }

    public required IReadOnlyList<string> Original { get; init; }
}

