namespace SpaceX.WebApi.Contracts.Responses;

public sealed record CrewResponse
{
    public required string CrewId { get; init; }

    public required string Role { get; init; }
}

