namespace SpaceX.WebApi.Contracts.Responses;

public sealed record CoreResponse
{
    public string? LandingType { get; init; }

    public string? Landpad { get; init; }
}

