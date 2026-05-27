using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using SpaceX.Infrastructure.ExternalApis.SpaceX.Options;
using SpaceX.Infrastructure.Interfaces.ExternalApis.SpaceX;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class SpaceXExternalApiServiceCollectionExtensions
{
    public static IServiceCollection AddSpaceXExternalApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.Configure<SpaceXOptions>(configuration.GetSection("SpaceXOptions"));

        services.AddHttpClient<ISpaceXApiClient, SpaceXApiClient>((serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<SpaceXOptions>>().Value;

            client.BaseAddress = new Uri(options.BaseAddress);
        });

        return services;
    }
}