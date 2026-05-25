namespace SpaceX.WebApi.Contracts.Responses;

public sealed record LoginResponse
{
    public required string AccessToken { get; init; }

    public required string RefreshToken { get; init; }

    public required AccountResponse Account { get; init; }
}

