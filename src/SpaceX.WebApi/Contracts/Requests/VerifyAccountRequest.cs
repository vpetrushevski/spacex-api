namespace SpaceX.WebApi.Contracts.Requests;

public sealed record VerifyAccountRequest
{
    public required Guid AccountId { get; init; }

    public required string Token { get; init; }
}
