namespace SpaceX.WebApi.Contracts.Responses;

public sealed record CoreResponse
{
    public string? CoreId { get; init; }

    public int? Flight { get; init; }

    public bool? Gridfins { get; init; }

    public bool? Legs { get; init; }

    public bool? Reused { get; init; }

    public bool? LandingAttempt { get; init; }

    public bool? LandingSuccess { get; init; }

    public string? LandingType { get; init; }

    public string? Landpad { get; init; }
}

