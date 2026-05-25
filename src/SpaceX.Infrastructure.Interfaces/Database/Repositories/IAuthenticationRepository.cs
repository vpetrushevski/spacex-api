using SpaceX.Core.Domain.Entities;

namespace SpaceX.Infrastructure.Interfaces.Database.Repositories;

public interface IAuthenticationRepository
{
    Task<RefreshToken?> GetRefreshTokenAsync(Guid accountId, string token, CancellationToken cancellationToken = default);

    Task CreateRefreshTokenAsync(RefreshToken request, CancellationToken cancellationToken = default);

    Task DeleteRefreshTokensByAccountIdAsync(Guid accountId, CancellationToken cancellationToken = default);

    Task DeleteExpiredRefreshTokensAsync(CancellationToken cancellationToken = default);

    Task CreatePasswordResetTokenAsync(PasswordResetToken request, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PasswordResetToken>> GetPasswordResetTokensAsync(Guid accountId, CancellationToken cancellationToken = default);

    Task<PasswordResetToken?> GetPasswordResetTokenByAccountIdAndHashedTokenAsync(Guid accountId, string hashedToken, CancellationToken cancellationToken = default);

    Task DeletePasswordResetTokensAsync(IReadOnlyList<PasswordResetToken> request, CancellationToken cancellationToken = default);
}

