namespace SpaceX.Core.Domain.Models.Authentication;

public class AuthenticatedUser
{
    public Guid AccountId { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }
}

