using System.Diagnostics.CodeAnalysis;

using Microsoft.Extensions.DependencyInjection;

using SpaceX.Core.Services.Accounts;
using SpaceX.Core.Services.Authentication;
using SpaceX.Core.Services.Helpers;
using SpaceX.Core.Services.Interfaces;
using SpaceX.Core.Services.Interfaces.Authentication;
using SpaceX.Core.Services.Launches;

namespace SpaceX.Core.Services.DependencyInjection;

[ExcludeFromCodeCoverage]
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

        services.AddMemoryCache();
        services.AddScoped<ILaunchService, LaunchService>();

        return services;
    }
}

