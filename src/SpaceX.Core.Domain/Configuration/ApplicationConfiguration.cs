namespace SpaceX.Core.Domain.Configuration;

public class ApplicationConfiguration
{
    public required string AppUrl { get; init; }

    public required string ApiUrl { get; init; }
}