namespace SpaceX.Core.Domain.Configuration;

public class EmailConfiguration
{
    public required string EmailAddress { get; init; }

    public required string DisplayName { get; init; }

    public required string Password { get; init; }

    public required string Host { get; init; }

    public int Port { get; init; }
}

