namespace SpaceX.Core.Domain.Models.Responses;

public class LoginResponse
{
    public required string AccessToken { get; init; }

    public required string RefreshToken { get; init; }

    public required AccountResponse Account { get; init; }
}

