namespace SpaceX.WebApi.Contracts.Requests;

public sealed record RefreshTokenRequest
{
    public required string AccessToken { get; init; }

    public required string RefreshToken { get; init; }
}
