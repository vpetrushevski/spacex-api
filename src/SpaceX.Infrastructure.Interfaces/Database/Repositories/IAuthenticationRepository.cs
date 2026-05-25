using SpaceX.Core.Domain.Entities;

namespace SpaceX.Infrastructure.Interfaces.Database.Repositories;

public interface IAuthenticationRepository
{
    Task<RefreshToken?> GetRefreshTokenAsync(Guid accountId, string token);

    Task AddRefreshTokenAsync(RefreshToken request);

    Task RemoveRefreshTokenAsync(Guid accountId, string token);

    Task RemoveExpiredRefreshTokensAsync();

    Task CreatePasswordResetTokenAsync(PasswordResetToken request);

    Task<IReadOnlyList<PasswordResetToken>> GetPasswordResetTokensAsync(Guid accountId);

    Task<PasswordResetToken?> GetPasswordResetTokenByAccountIdAndHashedTokenAsync(Guid accountId, string? hashedToken = null);

    Task DeletePasswordResetTokensAsync(IReadOnlyList<PasswordResetToken> request);
}

