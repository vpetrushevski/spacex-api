using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore;

using SpaceX.Core.Domain.Entities;
using SpaceX.Infrastructure.Database.Context;
using SpaceX.Infrastructure.Database.Mappings;
using SpaceX.Infrastructure.Database.Models;
using SpaceX.Infrastructure.Interfaces.Database.Repositories;

namespace SpaceX.Infrastructure.Database.Repositories;

[ExcludeFromCodeCoverage]
public class AccountRepository : IAccountRepository
{
    private readonly ISpaceXDbContext _context;

    public AccountRepository(ISpaceXDbContext context)
    {
        _context = context;
    }

    private IQueryable<AccountDataModel> Accounts => _context.Accounts.AsNoTracking();

    public async Task<Account?> GetAccountAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await Accounts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return entity?.ToDomain();
    }

    public async Task<Account?> GetAccountByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var entity = await Accounts.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

        return entity?.ToDomain();
    }

    public async Task CreateAccountAsync(Account request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var mappedRequest = request.ToDataModel();

        await _context.Accounts.AddAsync(mappedRequest, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAccountAsync(Account request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var entity = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException("Account does not exist.");

        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.Email = request.Email;
        entity.Password = request.Password;
        entity.Status = request.Status;
        entity.IsVerified = request.IsVerified;

        await _context.SaveChangesAsync(cancellationToken);
    }
}

