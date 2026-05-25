namespace SpaceX.Core.Domain.Entities;

public class PasswordResetToken
{
    public Guid Id { get; init; }

    public Guid AccountId { get; init; }

    public required string Token { get; init; }

    public DateTimeOffset ExpiresAtUtc { get; init; }
}
