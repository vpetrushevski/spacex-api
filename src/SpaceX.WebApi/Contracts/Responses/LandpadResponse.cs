namespace SpaceX.WebApi.Contracts.Responses;

public sealed record LandpadResponse : BasePadResponse
{
    public required string Type { get; init; }

    public int LandingAttempts { get; init; }

    public int LandingSuccesses { get; init; }

    public required string Wikipedia { get; init; }
}