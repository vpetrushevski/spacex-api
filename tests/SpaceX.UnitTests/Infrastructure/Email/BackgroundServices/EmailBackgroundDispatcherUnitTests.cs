using SpaceX.Core.Domain.Models.Email;
using SpaceX.Infrastructure.Email.BackgroundServices;

namespace SpaceX.UnitTests.Infrastructure.Email.BackgroundServices;

public class EmailBackgroundDispatcherTests
{
    private readonly EmailBackgroundDispatcher _sut;

    public EmailBackgroundDispatcherTests()
    {
        _sut = new EmailBackgroundDispatcher();
    }

    [Fact]
    public async Task EnqueueAsync_WhenMessageIsValid_WritesMessageToChannel()
    {
        // Arrange
        var message = CreateEmailMessage();

        // Act
        await _sut.EnqueueAsync(message, CancellationToken.None);

        var result = await ReadOneAsync(_sut.ReadAllAsync(CancellationToken.None));

        // Assert
        Assert.NotNull(result);
        Assert.Equal(message.Type, result.Type);
        Assert.Equal(message.Email, result.Email);
        Assert.Equal(message.FirstName, result.FirstName);
        Assert.Equal(message.LastName, result.LastName);
        Assert.Equal(message.AccountId, result.AccountId);
        Assert.Equal(message.Token, result.Token);
    }

    [Fact]
    public async Task EnqueueAsync_WhenMessageIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        EmailMessage? message = null;

        // Act
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(
            () => _sut.EnqueueAsync(message!, CancellationToken.None));

        // Assert
        Assert.Equal("message", exception.ParamName);
    }

    [Fact]
    public async Task ReadAllAsync_WhenMultipleMessagesAreQueued_ReturnsMessagesInSameOrder()
    {
        // Arrange
        var firstMessage = CreateEmailMessage(
            EmailType.PasswordChanged,
            "first@test.com",
            "Vlatko",
            "Petrushevski");

        var secondMessage = CreateEmailMessage(
            EmailType.ForgotPassword,
            "second@test.com",
            "John",
            "Doe");

        await _sut.EnqueueAsync(firstMessage, CancellationToken.None);
        await _sut.EnqueueAsync(secondMessage, CancellationToken.None);

        // Act
        var firstResult = await ReadOneAsync(_sut.ReadAllAsync(CancellationToken.None));
        var secondResult = await ReadOneAsync(_sut.ReadAllAsync(CancellationToken.None));

        // Assert
        Assert.NotNull(firstResult);
        Assert.NotNull(secondResult);

        Assert.Equal(firstMessage.Type, firstResult.Type);
        Assert.Equal(firstMessage.Email, firstResult.Email);
        Assert.Equal(firstMessage.FirstName, firstResult.FirstName);
        Assert.Equal(firstMessage.LastName, firstResult.LastName);

        Assert.Equal(secondMessage.Type, secondResult.Type);
        Assert.Equal(secondMessage.Email, secondResult.Email);
        Assert.Equal(secondMessage.FirstName, secondResult.FirstName);
        Assert.Equal(secondMessage.LastName, secondResult.LastName);
    }

    [Fact]
    public async Task ReadAllAsync_WhenCancellationTokenIsCancelled_ThrowsTaskCanceledException()
    {
        // Arrange
        using var cancellationTokenSource = new CancellationTokenSource();
        await cancellationTokenSource.CancelAsync();

        // Act
        var exception = await Assert.ThrowsAsync<TaskCanceledException>(
            () => ReadOneAsync(_sut.ReadAllAsync(cancellationTokenSource.Token)));

        // Assert
        Assert.NotNull(exception);
    }

    private static async Task<EmailMessage> ReadOneAsync(IAsyncEnumerable<EmailMessage> messages)
    {
        await foreach (var message in messages)
        {
            return message;
        }

        throw new InvalidOperationException("No email message was read.");
    }

    private static EmailMessage CreateEmailMessage(
        EmailType type = EmailType.Verification,
        string email = "test@test.com",
        string firstName = "Vlatko",
        string lastName = "Petrushevski")
    {
        return new EmailMessage
        {
            Type = type,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            AccountId = Guid.NewGuid(),
            Token = "token"
        };
    }
}