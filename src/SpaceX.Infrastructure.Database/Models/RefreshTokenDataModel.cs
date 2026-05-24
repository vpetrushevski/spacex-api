namespace SpaceX.Infrastructure.Database.Models;

public class RefreshTokenDataModel : BaseDataModel
{
    public Guid AccountId { get; set; }

    public required string Token { get; set; }

    public DateTimeOffset ExpiresAtUtc { get; set; }

    public AccountDataModel Account { get; set; } = null!;
}

