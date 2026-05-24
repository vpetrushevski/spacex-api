using Microsoft.EntityFrameworkCore;
using SpaceX.Core.Domain.Entities;
using SpaceX.Infrastructure.Database.Context;
using SpaceX.Infrastructure.Database.Mappings;
using SpaceX.Infrastructure.Database.Models;
using SpaceX.Infrastructure.Interfaces.Database.Repositories;

namespace SpaceX.Infrastructure.Database.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly ISpaceXDbContext _context;

    public AccountRepository(ISpaceXDbContext context)
    {
        _context = context;
    }

    private IQueryable<AccountDataModel> Accounts => _context.Accounts.AsNoTracking();

    public async Task<Account?> GetAccountAsync(Guid id)
    {
        var entity = await Accounts.FirstOrDefaultAsync(x => x.Id == id);

        return entity?.ToDomain();
    }

    public async Task<Account?> GetAccountByEmailAsync(string email)
    {
        var entity = await Accounts.FirstOrDefaultAsync(x => x.Email == email);

        return entity?.ToDomain();
    }

    public async Task CreateAccountAsync(Account request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var mappedRequest = request.ToDataModel();

        await _context.Accounts.AddAsync(mappedRequest);
        await _context.SaveChangesAsync();
    }
}

