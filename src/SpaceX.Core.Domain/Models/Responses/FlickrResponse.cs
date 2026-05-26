namespace SpaceX.Core.Domain.Models.Responses;

public class FlickrResponse
{
    public required IReadOnlyList<string> Small { get; init; }

    public required IReadOnlyList<string> Original { get; init; }
}

