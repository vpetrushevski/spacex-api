using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SpaceX.Infrastructure.Database.Context;
using SpaceX.Infrastructure.Database.Repositories;
using SpaceX.Infrastructure.Interfaces.Database.Repositories;

namespace SpaceX.Infrastructure.Database.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class DatabaseServiceCollectionExtensions
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, string connectionString)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddDbContext<ISpaceXDbContext, SpaceXDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();

        return services;
    }
}

