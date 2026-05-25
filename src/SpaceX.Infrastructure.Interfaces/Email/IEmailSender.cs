using SpaceX.Core.Domain.Models.Email;

namespace SpaceX.Infrastructure.Interfaces.Email;

public interface IEmailSender
{
    Task SendVerificationEmailAsync(EmailMessage message, CancellationToken cancellationToken = default);

    Task SendForgotPasswordEmailAsync(EmailMessage message, CancellationToken cancellationToken = default);

    Task SendPasswordChangedEmailAsync(EmailMessage message, CancellationToken cancellationToken = default);
}