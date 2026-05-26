using Microsoft.EntityFrameworkCore;

using SpaceX.Core.Domain.Entities;
using SpaceX.Infrastructure.Database.Context;
using SpaceX.Infrastructure.Database.Mappings;
using SpaceX.Infrastructure.Database.Models;
using SpaceX.Infrastructure.Interfaces.Database.Repositories;

namespace SpaceX.Infrastructure.Database.Repositories;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly ISpaceXDbContext _context;

    public AuthenticationRepository(ISpaceXDbContext context)
    {
        _context = context;
    }

    private IQueryable<RefreshTokenDataModel> RefreshTokens => _context.RefreshTokens.AsNoTracking();
    private IQueryable<PasswordResetTokenDataModel> PasswordResetTokens => _context.PasswordResetTokens.AsNoTracking();

    public async Task<RefreshToken?> GetRefreshTokenAsync(Guid accountId, string token, CancellationToken cancellationToken)
    {
        var entity = await RefreshTokens
            .Include(x => x.Account)
            .FirstOrDefaultAsync(x => x.AccountId == accountId && x.Token == token && x.ExpiresAtUtc >= DateTimeOffset.UtcNow, cancellationToken);

        return entity?.ToDomain();
    }

    public async Task CreateRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(refreshToken);

        await _context.RefreshTokens.AddAsync(refreshToken.ToDataModel(), cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteRefreshTokensByAccountIdAsync(Guid accountId, CancellationToken cancellationToken)
    {
        var tokens = await _context.RefreshTokens
            .Where(x => x.AccountId == accountId)
            .ToListAsync(cancellationToken);

        if (tokens.Count == 0)
        {
            return;
        }

        _context.RefreshTokens.RemoveRange(tokens);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task CreatePasswordResetTokenAsync(PasswordResetToken passwordResetToken, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(passwordResetToken);

        await _context.PasswordResetTokens.AddAsync(passwordResetToken.ToDataModel(), cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<PasswordResetToken>> GetPasswordResetTokensAsync(Guid accountId, CancellationToken cancellationToken)
    {
        var entities = await PasswordResetTokens
            .Where(x => x.AccountId == accountId)
            .ToListAsync(cancellationToken);

        return entities.Select(x => x.ToDomain()).ToList();
    }

    public async Task<PasswordResetToken?> GetPasswordResetTokenByAccountIdAndHashedTokenAsync(Guid accountId, string hashedToken, CancellationToken cancellationToken)
    {
        var entity = await PasswordResetTokens.FirstOrDefaultAsync(x => x.AccountId == accountId && x.Token == hashedToken, cancellationToken);

        return entity?.ToDomain();
    }

    public async Task DeletePasswordResetTokensAsync(IReadOnlyList<PasswordResetToken> passwordResetTokens, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(passwordResetTokens);

        if (passwordResetTokens.Count == 0)
        {
            return;
        }

        var ids = passwordResetTokens.Select(x => x.Id).ToList();

        var entities = await _context.PasswordResetTokens
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);

        if (entities.Count == 0)
        {
            return;
        }

        _context.PasswordResetTokens.RemoveRange(entities);
        await _context.SaveChangesAsync(cancellationToken);
    }
}