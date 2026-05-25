namespace SpaceX.Core.Domain.Models.Responses;

public class AccountResponse
{
    public Guid Id { get; set; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string Email { get; init; }
}

