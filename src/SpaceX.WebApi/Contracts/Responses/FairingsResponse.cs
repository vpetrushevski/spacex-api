namespace SpaceX.WebApi.Contracts.Responses;

public sealed record FairingsResponse
{
    public bool? Reused { get; init; }

    public bool? RecoveryAttempt { get; init; }

    public bool? Recovered { get; init; }

    public required IReadOnlyList<string> Ships { get; init; }
}

