using Microsoft.Extensions.Options;

using Moq;

using SpaceX.Core.Domain.Configuration;
using SpaceX.Core.Domain.Models.Email;
using SpaceX.Infrastructure.Email.Constants;
using SpaceX.Infrastructure.Email.Senders;
using SpaceX.Infrastructure.Interfaces.Email;

namespace SpaceX.UnitTests.Infrastructure.Email.Senders;

public class EmailSenderTests
{
    private readonly EmailSender _sut;

    private readonly Mock<ITemplateProvider> _templateProviderMock = new();

    public EmailSenderTests()
    {
        _sut = new EmailSender(
            Options.Create(CreateApplicationConfiguration()),
            Options.Create(CreateEmailConfiguration()),
            _templateProviderMock.Object);
    }

    [Fact]
    public void Constructor_WhenApplicationConfigurationIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        IOptions<ApplicationConfiguration>? applicationConfiguration = null;
        var emailConfiguration = Options.Create(CreateEmailConfiguration());

        // Act
        var exception = Assert.Throws<ArgumentNullException>(
            () => new EmailSender(
                applicationConfiguration!,
                emailConfiguration,
                _templateProviderMock.Object));

        // Assert
        Assert.Equal("applicationConfiguration", exception.ParamName);
    }

    [Fact]
    public void Constructor_WhenEmailConfigurationIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var applicationConfiguration = Options.Create(CreateApplicationConfiguration());
        IOptions<EmailConfiguration>? emailConfiguration = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(
            () => new EmailSender(
                applicationConfiguration,
                emailConfiguration!,
                _templateProviderMock.Object));

        // Assert
        Assert.Equal("emailConfiguration", exception.ParamName);
    }

    [Theory]
    [InlineData(EmailType.Verification)]
    [InlineData(EmailType.ForgotPassword)]
    [InlineData(EmailType.PasswordChanged)]
    public async Task SendEmailAsync_WhenMessageIsNull_ThrowsArgumentNullException(EmailType emailType)
    {
        // Arrange
        EmailMessage? message = null;

        // Act
        var exception = emailType switch
        {
            EmailType.Verification => await Assert.ThrowsAsync<ArgumentNullException>(
                () => _sut.SendVerificationEmailAsync(message!, CancellationToken.None)),

            EmailType.ForgotPassword => await Assert.ThrowsAsync<ArgumentNullException>(
                () => _sut.SendForgotPasswordEmailAsync(message!, CancellationToken.None)),

            EmailType.PasswordChanged => await Assert.ThrowsAsync<ArgumentNullException>(
                () => _sut.SendPasswordChangedEmailAsync(message!, CancellationToken.None)),

            _ => throw new InvalidOperationException()
        };

        // Assert
        Assert.Equal("message", exception.ParamName);
    }

    [Fact]
    public async Task SendVerificationEmailAsync_WhenTemplateProviderThrowsException_UsesVerificationTemplateAndParameters()
    {
        // Arrange
        var message = CreateEmailMessage();
        var applicationConfiguration = CreateApplicationConfiguration();

        _templateProviderMock
            .Setup(x => x.GetTemplateAsync(
                TemplateFolderConstants.Account,
                TemplateNameConstants.VerifyAccountTemplate,
                It.Is<IReadOnlyDictionary<string, string?>>(parameters =>
                    parameters["@firstName"] == message.FirstName &&
                    parameters["@lastName"] == message.LastName &&
                    parameters["@verificationLink"] ==
                    $"{applicationConfiguration.AppUrl}/auth/verify?uid={message.AccountId}&token={Uri.EscapeDataString(message.Token!)}"),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Template failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.SendVerificationEmailAsync(message, CancellationToken.None));

        // Assert
        Assert.Equal("Template failed.", exception.Message);

        _templateProviderMock.Verify(
            x => x.GetTemplateAsync(
                TemplateFolderConstants.Account,
                TemplateNameConstants.VerifyAccountTemplate,
                It.IsAny<IReadOnlyDictionary<string, string?>>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task SendForgotPasswordEmailAsync_WhenTemplateProviderThrowsException_UsesForgotPasswordTemplateAndParameters()
    {
        // Arrange
        var message = CreateEmailMessage();
        var applicationConfiguration = CreateApplicationConfiguration();

        _templateProviderMock
            .Setup(x => x.GetTemplateAsync(
                TemplateFolderConstants.Account,
                TemplateNameConstants.ForgotPasswordTemplate,
                It.Is<IReadOnlyDictionary<string, string?>>(parameters =>
                    parameters["@firstName"] == message.FirstName &&
                    parameters["@lastName"] == message.LastName &&
                    parameters["@resetLink"] ==
                    $"{applicationConfiguration.AppUrl}/auth/reset-password?uid={message.AccountId}&token={Uri.EscapeDataString(message.Token!)}"),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Template failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.SendForgotPasswordEmailAsync(message, CancellationToken.None));

        // Assert
        Assert.Equal("Template failed.", exception.Message);

        _templateProviderMock.Verify(
            x => x.GetTemplateAsync(
                TemplateFolderConstants.Account,
                TemplateNameConstants.ForgotPasswordTemplate,
                It.IsAny<IReadOnlyDictionary<string, string?>>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task SendPasswordChangedEmailAsync_WhenTemplateProviderThrowsException_UsesPasswordChangedTemplateAndParameters()
    {
        // Arrange
        var message = CreateEmailMessage();

        _templateProviderMock
            .Setup(x => x.GetTemplateAsync(
                TemplateFolderConstants.Account,
                TemplateNameConstants.PasswordChangedTemplate,
                It.Is<IReadOnlyDictionary<string, string?>>(parameters =>
                    parameters["@firstName"] == message.FirstName &&
                    parameters["@lastName"] == message.LastName),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Template failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.SendPasswordChangedEmailAsync(message, CancellationToken.None));

        // Assert
        Assert.Equal("Template failed.", exception.Message);

        _templateProviderMock.Verify(
            x => x.GetTemplateAsync(
                TemplateFolderConstants.Account,
                TemplateNameConstants.PasswordChangedTemplate,
                It.IsAny<IReadOnlyDictionary<string, string?>>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    private static ApplicationConfiguration CreateApplicationConfiguration()
    {
        return new ApplicationConfiguration
        {
            AppUrl = "http://localhost:4200",
            ApiUrl = "http://localhost:7019"
        };
    }

    private static EmailConfiguration CreateEmailConfiguration()
    {
        return new EmailConfiguration
        {
            EmailAddress = "test@test.com",
            DisplayName = "Space X | Hornet Security",
            Password = "password",
            Host = "smtp.gmail.com",
            Port = 465
        };
    }

    private static EmailMessage CreateEmailMessage()
    {
        return new EmailMessage
        {
            Type = EmailType.Verification,
            Email = "test@test.com",
            FirstName = "Vlatko",
            LastName = "Petrushevski",
            AccountId = Guid.NewGuid(),
            Token = "token"
        };
    }
}