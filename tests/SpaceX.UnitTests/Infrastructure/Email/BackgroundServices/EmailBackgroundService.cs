using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Moq;

using SpaceX.Core.Domain.Models.Email;
using SpaceX.Infrastructure.Email.BackgroundServices;
using SpaceX.Infrastructure.Interfaces.Email;

namespace SpaceX.UnitTests.Infrastructure.Email.BackgroundServices;

public class EmailBackgroundServiceTests
{
    private readonly TestableEmailBackgroundService _sut;

    private readonly EmailBackgroundDispatcher _emailBackgroundDispatcher = new();
    private readonly Mock<IServiceScopeFactory> _serviceScopeFactoryMock = new();
    private readonly Mock<IServiceScope> _serviceScopeMock = new();
    private readonly Mock<IServiceProvider> _serviceProviderMock = new();
    private readonly Mock<IEmailSender> _emailSenderMock = new();
    private readonly Mock<ILogger<EmailBackgroundService>> _loggerMock = new();

    public EmailBackgroundServiceTests()
    {
        _serviceScopeMock
            .Setup(x => x.ServiceProvider)
            .Returns(_serviceProviderMock.Object);

        _serviceProviderMock
            .Setup(x => x.GetService(typeof(IEmailSender)))
            .Returns(_emailSenderMock.Object);

        _serviceScopeFactoryMock
            .Setup(x => x.CreateScope())
            .Returns(_serviceScopeMock.Object);

        _sut = new TestableEmailBackgroundService(
            _emailBackgroundDispatcher,
            _serviceScopeFactoryMock.Object,
            _loggerMock.Object);
    }

    [Theory]
    [InlineData(EmailType.Verification)]
    [InlineData(EmailType.ForgotPassword)]
    [InlineData(EmailType.PasswordChanged)]
    public async Task ExecuteAsync_WhenEmailTypeIsSupported_SendsExpectedEmail(EmailType emailType)
    {
        // Arrange
        using var cancellationTokenSource = new CancellationTokenSource();

        var message = CreateEmailMessage(emailType);

        switch (emailType)
        {
            case EmailType.Verification:
                _emailSenderMock
                    .Setup(x => x.SendVerificationEmailAsync(message, It.IsAny<CancellationToken>()))
                    .Callback(cancellationTokenSource.Cancel)
                    .Returns(Task.CompletedTask);
                break;

            case EmailType.ForgotPassword:
                _emailSenderMock
                    .Setup(x => x.SendForgotPasswordEmailAsync(message, It.IsAny<CancellationToken>()))
            .       Callback(cancellationTokenSource.Cancel)
                    .Returns(Task.CompletedTask);
                break;

            case EmailType.PasswordChanged:
                _emailSenderMock
                    .Setup(x => x.SendPasswordChangedEmailAsync(message, It.IsAny<CancellationToken>()))
                    .Callback(cancellationTokenSource.Cancel)
                    .Returns(Task.CompletedTask);
                break;
        }

        await _emailBackgroundDispatcher.EnqueueAsync(message, CancellationToken.None);

        // Act
        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => _sut.ExecuteForTestAsync(cancellationTokenSource.Token));

        // Assert
        switch (emailType)
        {
            case EmailType.Verification:
                _emailSenderMock.Verify(
                    x => x.SendVerificationEmailAsync(message, It.IsAny<CancellationToken>()),
                    Times.Once);
                break;

            case EmailType.ForgotPassword:
                _emailSenderMock.Verify(
                    x => x.SendForgotPasswordEmailAsync(message, It.IsAny<CancellationToken>()),
                    Times.Once);
                break;

            case EmailType.PasswordChanged:
                _emailSenderMock.Verify(
                    x => x.SendPasswordChangedEmailAsync(message, It.IsAny<CancellationToken>()),
                    Times.Once);
                break;
        }
    }

    [Fact]
    public async Task ExecuteAsync_WhenEmailTypeIsUnsupported_LogsWarning()
    {
        // Arrange
        using var cancellationTokenSource = new CancellationTokenSource();

        var message = CreateEmailMessage((EmailType)999);

        await _emailBackgroundDispatcher.EnqueueAsync(message, CancellationToken.None);
        cancellationTokenSource.CancelAfter(100);

        // Act
        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => _sut.ExecuteForTestAsync(cancellationTokenSource.Token));

        // Assert
        _emailSenderMock.Verify(
            x => x.SendVerificationEmailAsync(It.IsAny<EmailMessage>(), It.IsAny<CancellationToken>()),
            Times.Never);

        _emailSenderMock.Verify(
            x => x.SendForgotPasswordEmailAsync(It.IsAny<EmailMessage>(), It.IsAny<CancellationToken>()),
            Times.Never);

        _emailSenderMock.Verify(
            x => x.SendPasswordChangedEmailAsync(It.IsAny<EmailMessage>(), It.IsAny<CancellationToken>()),
            Times.Never);

        VerifyLog(LogLevel.Warning, "Unsupported email type", Times.Once());
    }

    [Fact]
    public async Task ExecuteAsync_WhenEmailSenderThrowsException_LogsError()
    {
        // Arrange
        using var cancellationTokenSource = new CancellationTokenSource();

        var message = CreateEmailMessage(EmailType.Verification);

        _emailSenderMock
            .Setup(x => x.SendVerificationEmailAsync(message, It.IsAny<CancellationToken>()))
            .Callback(cancellationTokenSource.Cancel)
            .ThrowsAsync(new InvalidOperationException("Email send failed."));

        await _emailBackgroundDispatcher.EnqueueAsync(message, CancellationToken.None);

        // Act
        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => _sut.ExecuteForTestAsync(cancellationTokenSource.Token));

        // Assert
        VerifyLog(LogLevel.Error, "Failed to send", Times.Once());
    }

    private void VerifyLog(LogLevel logLevel, string message, Times times)
    {
        _loggerMock.Verify(
            x => x.Log(
                logLevel,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((value, _) => value.ToString()!.Contains(message)),
                It.IsAny<Exception?>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            times);
    }

    private static EmailMessage CreateEmailMessage(EmailType type)
    {
        return new EmailMessage
        {
            Type = type,
            Email = "test@test.com",
            FirstName = "Vlatko",
            LastName = "Petrushevski",
            AccountId = Guid.NewGuid(),
            Token = "token"
        };
    }

    private sealed class TestableEmailBackgroundService : EmailBackgroundService
    {
        public TestableEmailBackgroundService(
            EmailBackgroundDispatcher emailBackgroundDispatcher,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<EmailBackgroundService> logger)
            : base(emailBackgroundDispatcher, serviceScopeFactory, logger)
        {
        }

        public Task ExecuteForTestAsync(CancellationToken cancellationToken)
        {
            return ExecuteAsync(cancellationToken);
        }
    }
}