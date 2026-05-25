using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SpaceX.Core.Domain.Configuration;

namespace SpaceX.Core.Services.DependencyInjection;

public static class ConfigurationServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EncryptionConfiguration>(configuration.GetSection("EncryptionConfiguration"));
        services.Configure<JwtTokenConfiguration>(configuration.GetSection("JwtTokenConfiguration"));

        return services;
    }
}

