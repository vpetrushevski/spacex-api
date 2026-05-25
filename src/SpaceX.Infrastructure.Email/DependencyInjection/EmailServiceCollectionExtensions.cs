using Microsoft.Extensions.DependencyInjection;

using SpaceX.Infrastructure.Email.BackgroundServices;
using SpaceX.Infrastructure.Email.Providers;
using SpaceX.Infrastructure.Email.Senders;
using SpaceX.Infrastructure.Interfaces.Email;

namespace SpaceX.Infrastructure.Email.DependencyInjection;

public static class EmailServiceCollectionExtensions
{
    public static IServiceCollection AddEmailServices(this IServiceCollection services)
    {
        services.AddSingleton<EmailBackgroundDispatcher>();

        services.AddSingleton<IEmailBackgroundDispatcher>(provider => provider.GetRequiredService<EmailBackgroundDispatcher>());

        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<ITemplateProvider, TemplateProvider>();

        services.AddHostedService<EmailBackgroundService>();

        return services;
    }
}