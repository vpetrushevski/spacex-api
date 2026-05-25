using Microsoft.Extensions.DependencyInjection;

using SpaceX.Core.Services.Accounts;
using SpaceX.Core.Services.Authentication;
using SpaceX.Core.Services.Helpers;
using SpaceX.Core.Services.Interfaces;
using SpaceX.Core.Services.Interfaces.Authentication;

namespace SpaceX.Core.Services.DependencyInjection;

public static class BusinessServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<EncryptionHelper>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<ITokenService, TokenService>();

        return services;
    }
}

