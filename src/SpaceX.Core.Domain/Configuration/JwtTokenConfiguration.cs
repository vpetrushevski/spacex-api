namespace SpaceX.Core.Domain.Configuration;

public class JwtTokenConfiguration
{
    public required string Secret { get; init; }

    public int TokenValidityInMinutes { get; init; }

    public int RefreshTokenValidityInDays { get; init; }

    public required string ValidIssuer { get; init; }

    public required string ValidAudience { get; init; }
}

