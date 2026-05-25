using SpaceX.Core.Domain.Entities;

namespace SpaceX.Infrastructure.Interfaces.Database.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetAccountAsync(Guid id);

    Task<Account?> GetAccountByEmailAsync(string email);

    Task CreateAccountAsync(Account request);

    Task UpdateAccountAsync(Account request);
}
