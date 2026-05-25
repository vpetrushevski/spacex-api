using SpaceX.Core.Domain.Entities;

namespace SpaceX.Infrastructure.Interfaces.Database.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetAccountAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Account?> GetAccountByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task CreateAccountAsync(Account request, CancellationToken cancellationToken = default);

    Task UpdateAccountAsync(Account request, CancellationToken cancellationToken = default);
}
