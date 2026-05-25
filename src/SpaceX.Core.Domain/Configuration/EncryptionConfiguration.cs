namespace SpaceX.Core.Domain.Configuration;

public class EncryptionConfiguration
{
    public required string EncryptionKey { get; init; }

    public required string InitializationVector { get; init; }
}

