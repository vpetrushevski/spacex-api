using SpaceX.Core.Domain.Models.Email;

namespace SpaceX.Infrastructure.Interfaces.Email;

public interface IEmailBackgroundDispatcher
{
    Task EnqueueAsync(EmailMessage message);
}