using SpaceX.Core.Domain.Models.Email;

namespace SpaceX.Infrastructure.Interfaces.Email;

public interface IEmailSender
{
    Task SendVerificationEmailAsync(EmailMessage message, CancellationToken cancellationToken);

    Task SendForgotPasswordEmailAsync(EmailMessage message, CancellationToken cancellationToken);

    Task SendPasswordChangedEmailAsync(EmailMessage message, CancellationToken cancellationToken);
}