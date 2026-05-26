namespace SpaceX.WebApi.Contracts.Responses;

public sealed record RedditResponse
{
    public string? Campaign { get; init; }

    public string? Launch { get; init; }

    public string? Media { get; init; }

    public string? Recovery { get; init; }

}

