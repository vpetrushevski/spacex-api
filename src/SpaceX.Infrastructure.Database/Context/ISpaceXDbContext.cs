using Microsoft.EntityFrameworkCore;
using SpaceX.Infrastructure.Database.Models;

namespace SpaceX.Infrastructure.Database.Context;

public interface ISpaceXDbContext
{
    DbSet<AccountDataModel> Accounts { get; }

    DbSet<PasswordResetTokenDataModel> PasswordResetTokens { get; }

    DbSet<RefreshTokenDataModel> RefreshTokens { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}