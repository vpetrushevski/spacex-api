using SpaceX.Core.Domain.Entities.Enums;

namespace SpaceX.Core.Domain.Entities;

public class Account
{
    public Guid Id { get; set; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string Email { get; init; }

    public required string Password { get; set; }

    public AccountStatus Status { get; set; }

    public bool IsVerified { get; set; }

    public string? VerificationToken { get; set; }

    public DateTimeOffset CreatedAtUtc { get; init; }
}

