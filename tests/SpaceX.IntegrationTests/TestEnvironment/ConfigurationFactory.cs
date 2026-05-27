using Microsoft.Extensions.Configuration;

namespace SpaceX.IntegrationTests.TestEnvironment;

public static class ConfigurationFactory
{
    public static IConfiguration Create() =>
        new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"), optional: false)
            .AddJsonFile(Path.Combine(AppContext.BaseDirectory, $"appsettings.{GetEnvironment()}.json"), optional: true)
            .Build();

    private static string GetEnvironment() =>
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;
}