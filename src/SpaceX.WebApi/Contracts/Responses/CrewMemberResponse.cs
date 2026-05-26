namespace SpaceX.WebApi.Contracts.Responses;

public sealed record CrewMemberResponse
{
    public required string Name { get; init; }

    public required string Agency { get; init; }

    public string? Image { get; init; }

    public string? Wikipedia { get; init; }

    public required string Status { get; init; }

    public required string Id { get; init; }
}