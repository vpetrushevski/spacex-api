using MailKit.Net.Smtp;

using Microsoft.Extensions.Options;

using MimeKit;

using SpaceX.Core.Domain.Configuration;
using SpaceX.Core.Domain.Models.Email;
using SpaceX.Infrastructure.Email.Constants;
using SpaceX.Infrastructure.Interfaces.Email;

namespace SpaceX.Infrastructure.Email.Senders;

public class EmailSender : IEmailSender
{
    private readonly ApplicationConfiguration _applicationConfiguration;
    private readonly EmailConfiguration _emailConfiguration;
    private readonly ITemplateProvider _templateProvider;

    public EmailSender(
        IOptions<ApplicationConfiguration> applicationConfiguration,
        IOptions<EmailConfiguration> emailConfiguration,
        ITemplateProvider templateProvider)
    {
        ArgumentNullException.ThrowIfNull(applicationConfiguration);
        ArgumentNullException.ThrowIfNull(emailConfiguration);

        _applicationConfiguration = applicationConfiguration.Value;
        _emailConfiguration = emailConfiguration.Value;
        _templateProvider = templateProvider;
    }

    public async Task SendVerificationEmailAsync(EmailMessage message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);

        var verificationLink = $"{_applicationConfiguration.AppUrl}/auth/verify?uid={message.AccountId}&token={message.Token}";

        var parameters = new Dictionary<string, string?>
        {
            { "@firstName", message.FirstName },
            { "@lastName", message.LastName },
            { "@verificationLink", verificationLink }
        };

        var html = await _templateProvider.GetTemplateAsync(TemplateFolderConstants.Account, TemplateNameConstants.VerifyAccountTemplate, parameters, cancellationToken);

        await SendAsync(message.Email, EmailSubjectConstants.VerifyAccountSubject, html, cancellationToken);
    }

    public async Task SendForgotPasswordEmailAsync(EmailMessage message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);

        var resetLink = $"{_applicationConfiguration.AppUrl}/auth/reset-password?uid={message.AccountId}&token={message.Token}";

        var parameters = new Dictionary<string, string?>
        {
            { "@firstName", message.FirstName },
            { "@lastName", message.LastName },
            { "@resetLink", resetLink }
        };

        var html = await _templateProvider.GetTemplateAsync(TemplateFolderConstants.Account, TemplateNameConstants.ForgotPasswordTemplate, parameters, cancellationToken);

        await SendAsync(message.Email, EmailSubjectConstants.ResetPasswordSubject, html, cancellationToken);
    }

    public async Task SendPasswordChangedEmailAsync(EmailMessage message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);

        var parameters = new Dictionary<string, string?>
        {
            { "@firstName", message.FirstName },
            { "@lastName", message.LastName }
        };

        var html = await _templateProvider.GetTemplateAsync(TemplateFolderConstants.Account, TemplateNameConstants.PasswordChangedTemplate, parameters, cancellationToken);

        await SendAsync(message.Email, EmailSubjectConstants.PasswordChangedSubject, html, cancellationToken);
    }

    private async Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(to);
        ArgumentException.ThrowIfNullOrWhiteSpace(subject);
        ArgumentException.ThrowIfNullOrWhiteSpace(body);

        var mailMessage = new MimeMessage();

        mailMessage.From.Add(new MailboxAddress(_emailConfiguration.DisplayName, _emailConfiguration.EmailAddress));
        mailMessage.To.Add(new MailboxAddress(to, to));
        mailMessage.Subject = subject;

        mailMessage.Body = new BodyBuilder
        {
            HtmlBody = body,
            TextBody = body
        }.ToMessageBody();

        using var smtpClient = new SmtpClient();

        await smtpClient.ConnectAsync(_emailConfiguration.Host, _emailConfiguration.Port, true, cancellationToken);
        await smtpClient.AuthenticateAsync(_emailConfiguration.EmailAddress, _emailConfiguration.Password, cancellationToken);
        await smtpClient.SendAsync(mailMessage, cancellationToken);
        await smtpClient.DisconnectAsync(true, cancellationToken);
    }
}