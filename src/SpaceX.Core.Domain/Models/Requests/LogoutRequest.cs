namespace SpaceX.Core.Domain.Models.Requests;

public class LogoutRequest
{
    public required string RefreshToken { get; init; }
}

