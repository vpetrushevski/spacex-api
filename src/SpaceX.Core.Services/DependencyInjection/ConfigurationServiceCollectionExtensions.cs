using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SpaceX.Core.Domain.Configuration;

namespace SpaceX.Core.Services.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ConfigurationServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApplicationConfiguration>(configuration.GetSection("ApplicationConfiguration"));
        services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));
        services.Configure<EncryptionConfiguration>(configuration.GetSection("EncryptionConfiguration"));
        services.Configure<JwtTokenConfiguration>(configuration.GetSection("JwtTokenConfiguration"));
        services.Configure<CacheConfiguration>(configuration.GetSection("CacheConfiguration"));

        return services;
    }
}

