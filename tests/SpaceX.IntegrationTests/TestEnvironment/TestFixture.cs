using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SpaceX.Infrastructure.ExternalApis.SpaceX.DependencyInjection;

namespace SpaceX.IntegrationTests.TestEnvironment;

public class TestFixture
{
    public TestFixture()
    {
        Configuration = ConfigurationFactory.Create();

        var services = new ServiceCollection();

        services
            .AddLogging()
            .AddSpaceXExternalApiConfiguration(Configuration);

        ServiceProvider = services.BuildServiceProvider();
    }

    public IConfiguration Configuration { get; }

    public IServiceProvider ServiceProvider { get; }
}