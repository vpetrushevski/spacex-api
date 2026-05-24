using Microsoft.Extensions.DependencyInjection;
using SpaceX.Core.Services.Accounts;
using SpaceX.Core.Services.Helpers;
using SpaceX.Core.Services.Interfaces;

namespace SpaceX.Core.Services.DependencyInjection;

public static class BusinessServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<EncryptionHelper>();

        services.AddTransient<IAccountService, AccountService>();

        return services;
    }
}

