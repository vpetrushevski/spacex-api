namespace SpaceX.WebApi.Contracts.Responses;

public sealed record PatchResponse
{
    public string? Small { get; init; }

    public string? Large { get; init; }
}

