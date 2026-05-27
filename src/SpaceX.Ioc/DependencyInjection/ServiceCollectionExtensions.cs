using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SpaceX.Core.Services.DependencyInjection;
using SpaceX.Infrastructure.Database.DependencyInjection;
using SpaceX.Infrastructure.Email.DependencyInjection;
using SpaceX.Infrastructure.ExternalApis.SpaceX.DependencyInjection;

namespace SpaceX.Ioc.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        var databaseConnectionString = configuration.GetConnectionString("DatabaseConnectionString")
            ?? throw new InvalidOperationException("Database connection string is required");

        services.AddDatabaseServices(databaseConnectionString);

        services.AddConfiguration(configuration);

        services.AddBusinessServices();
        services.AddEmailServices();

        services.AddSpaceXExternalApiConfiguration(configuration);

        return services;
    }
}

