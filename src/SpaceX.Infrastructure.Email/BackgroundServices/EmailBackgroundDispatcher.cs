using System.Threading.Channels;

using SpaceX.Core.Domain.Models.Email;
using SpaceX.Infrastructure.Interfaces.Email;

namespace SpaceX.Infrastructure.Email.BackgroundServices;

public class EmailBackgroundDispatcher : IEmailBackgroundDispatcher
{
    private readonly Channel<EmailMessage> _channel = Channel.CreateUnbounded<EmailMessage>();

    public async Task EnqueueAsync(EmailMessage message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message);

        await _channel.Writer.WriteAsync(message, cancellationToken);
    }

    public IAsyncEnumerable<EmailMessage> ReadAllAsync(CancellationToken cancellationToken)
    {
        return _channel.Reader.ReadAllAsync(cancellationToken);
    }
}

