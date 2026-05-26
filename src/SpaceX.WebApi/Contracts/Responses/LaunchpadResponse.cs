namespace SpaceX.WebApi.Contracts.Responses;

public sealed record LaunchpadResponse : BasePadResponse
{
    public int LaunchAttempts { get; init; }

    public int LaunchSuccesses { get; init; }

    public required IReadOnlyList<string> Rockets { get; init; }

    public required string Timezone { get; init; }
}