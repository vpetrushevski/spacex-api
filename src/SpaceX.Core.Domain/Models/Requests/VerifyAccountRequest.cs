namespace SpaceX.Core.Domain.Models.Requests;

public class VerifyAccountRequest
{
    public required Guid AccountId { get; init; }

    public required string Token { get; init; }
}

