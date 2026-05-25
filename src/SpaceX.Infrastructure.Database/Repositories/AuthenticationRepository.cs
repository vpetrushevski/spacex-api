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

    public async Task<RefreshToken?> GetRefreshTokenAsync(Guid accountId, string token)
    {
        var entity = await RefreshTokens
            .Include(x => x.Account)
            .FirstOrDefaultAsync(x => x.AccountId == accountId && x.Token == token && x.ExpiresAtUtc >= DateTimeOffset.UtcNow);

        return entity?.ToDomain();
    }

    public async Task CreateRefreshTokenAsync(RefreshToken refreshToken)
    {
        ArgumentNullException.ThrowIfNull(refreshToken);

        await _context.RefreshTokens.AddAsync(refreshToken.ToDataModel());
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRefreshTokenAsync(Guid accountId, string token)
    {
        var entity = await RefreshTokens.FirstOrDefaultAsync(x => x.AccountId == accountId && x.Token == token);

        if (entity is null)
        {
            return;
        }

        _context.RefreshTokens.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRefreshTokensByAccountIdAsync(Guid accountId)
    {
        var tokens = await _context.RefreshTokens
            .Where(x => x.AccountId == accountId)
            .ToListAsync();

        if (tokens.Count == 0)
        {
            return;
        }

        _context.RefreshTokens.RemoveRange(tokens);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteExpiredRefreshTokensAsync()
    {
        var tokensToRemove = await _context.RefreshTokens
            .Where(x => x.ExpiresAtUtc < DateTimeOffset.UtcNow)
            .ToListAsync();

        if (tokensToRemove.Count == 0)
        {
            return;
        }

        _context.RefreshTokens.RemoveRange(tokensToRemove);
        await _context.SaveChangesAsync();
    }

    public async Task CreatePasswordResetTokenAsync(PasswordResetToken passwordResetToken)
    {
        ArgumentNullException.ThrowIfNull(passwordResetToken);

        await _context.PasswordResetTokens.AddAsync(passwordResetToken.ToDataModel());
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<PasswordResetToken>> GetPasswordResetTokensAsync(Guid accountId)
    {
        var entities = await PasswordResetTokens
            .Where(x => x.AccountId == accountId)
            .ToListAsync();

        return entities.Select(x => x.ToDomain()).ToList();
    }

    public async Task<PasswordResetToken?> GetPasswordResetTokenByAccountIdAndHashedTokenAsync(Guid accountId, string? hashedToken = null)
    {
        var query = PasswordResetTokens.Where(x => x.AccountId == accountId);

        if (!string.IsNullOrWhiteSpace(hashedToken))
        {
            query = query.Where(x => x.Token == hashedToken);
        }

        var entity = await query.FirstOrDefaultAsync();

        return entity?.ToDomain();
    }

    public async Task DeletePasswordResetTokensAsync(IReadOnlyList<PasswordResetToken> passwordResetTokens)
    {
        ArgumentNullException.ThrowIfNull(passwordResetTokens);

        if (passwordResetTokens.Count == 0)
        {
            return;
        }

        var ids = passwordResetTokens.Select(x => x.Id).ToList();

        var entities = await _context.PasswordResetTokens
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();

        if (entities.Count == 0)
        {
            return;
        }

        _context.PasswordResetTokens.RemoveRange(entities);
        await _context.SaveChangesAsync();
    }
}