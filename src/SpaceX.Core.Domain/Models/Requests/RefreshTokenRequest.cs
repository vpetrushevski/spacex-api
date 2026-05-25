namespace SpaceX.Core.Domain.Models.Requests;

public class RefreshTokenRequest
{
    public required string AccessToken { get; init; }

    public required string RefreshToken { get; init; }
}

