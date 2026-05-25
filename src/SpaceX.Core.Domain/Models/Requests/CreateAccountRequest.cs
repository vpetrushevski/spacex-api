namespace SpaceX.Core.Domain.Models.Requests;

public class CreateAccountRequest
{
    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; }
}

