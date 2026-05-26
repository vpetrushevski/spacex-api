namespace SpaceX.Core.Domain.Models.Responses;

public class LandpadResponse : BasePadResponse
{
    public required string Type { get; init; }

    public int LandingAttempts { get; init; }

    public int LandingSuccesses { get; init; }

    public required string Wikipedia { get; init; }
}