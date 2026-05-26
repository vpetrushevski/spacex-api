namespace SpaceX.Core.Domain.Models.Responses;

public class FairingsResponse
{
    public bool? Reused { get; init; }

    public bool? RecoveryAttempt { get; init; }

    public bool? Recovered { get; init; }

    public required IReadOnlyList<string> Ships { get; init; }
}

