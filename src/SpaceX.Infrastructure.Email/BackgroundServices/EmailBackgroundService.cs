using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SpaceX.Core.Domain.Models.Email;
using SpaceX.Infrastructure.Interfaces.Email;

namespace SpaceX.Infrastructure.Email.BackgroundServices;

public class EmailBackgroundService : BackgroundService
{
    private readonly EmailBackgroundDispatcher _emailBackgroundDispatcher;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<EmailBackgroundService> _logger;

    public EmailBackgroundService(EmailBackgroundDispatcher emailBackgroundDispatcher, IServiceScopeFactory serviceScopeFactory, ILogger<EmailBackgroundService> logger)
    {
        _emailBackgroundDispatcher = emailBackgroundDispatcher;
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await foreach (var message in _emailBackgroundDispatcher.ReadAllAsync(cancellationToken))
        {
            try
            {
                using var scope = _serviceScopeFactory.CreateScope();

                var emailSender = scope.ServiceProvider.GetRequiredService<IEmailSender>();

                await (message.Type switch
                {
                    EmailType.Verification => emailSender.SendVerificationEmailAsync(message, cancellationToken),
                    EmailType.ForgotPassword => emailSender.SendForgotPasswordEmailAsync(message, cancellationToken),
                    EmailType.PasswordChanged => emailSender.SendPasswordChangedEmailAsync(message, cancellationToken),
                    _ => LogUnsupportedEmailTypeAsync(message)
                });
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to send {EmailType} email to {Email}.", message.Type, message.Email);
            }
        }
    }

    private Task LogUnsupportedEmailTypeAsync(EmailMessage message)
    {
        _logger.LogWarning("Unsupported email type: {EmailType}", message.Type);

        return Task.CompletedTask;
    }
}