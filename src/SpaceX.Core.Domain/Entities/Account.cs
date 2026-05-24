using SpaceX.Core.Domain.Entities.Enums;

namespace SpaceX.Core.Domain.Entities;

public class Account
{
    public Guid Id { get; set; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string Email { get; init; }

    public required string Password { get; init; }

    public AccountStatus Status { get; init; }

    public bool IsVerified { get; init; }

    public DateTimeOffset CreatedAtUtc { get; set; }
}

