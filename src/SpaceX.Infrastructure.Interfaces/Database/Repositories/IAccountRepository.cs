using SpaceX.Core.Domain.Entities;
using SpaceX.Core.Domain.Models.Requests;

namespace SpaceX.Infrastructure.Interfaces.Database.Repositories;

public interface IAccountRepository
{
    Task<Account?> GetAccountAsync(Guid id);

    Task<Account?> GetAccountByEmailAsync(string email);

    Task CreateAccountAsync(Account request);
}