namespace SpaceX.WebApi.Contracts.Responses;

public sealed record AccountResponse
{
    public Guid Id { get; set; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string Email { get; init; }
}

