namespace SpaceX.Core.Domain.Models.Email;

public record EmailMessage
{
    public required EmailType Type { get; init; }

    public required string Email { get; init; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public Guid? AccountId { get; init; }

    public string? Token { get; init; }
}

