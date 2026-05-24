using Microsoft.EntityFrameworkCore;
using SpaceX.Infrastructure.Database.Configuration;
using SpaceX.Infrastructure.Database.Models;

namespace SpaceX.Infrastructure.Database.Context;

public class SpaceXDbContext : DbContext, ISpaceXDbContext
{   
    public SpaceXDbContext(DbContextOptions<SpaceXDbContext> options) : base(options)
    { }

    public DbSet<AccountDataModel> Accounts => Set< AccountDataModel>();
    public DbSet<PasswordResetTokenDataModel> PasswordResetTokens => Set<PasswordResetTokenDataModel>();
    public DbSet<RefreshTokenDataModel> RefreshTokens => Set<RefreshTokenDataModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfiguration(new AccountEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PasswordResetTokenEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenEntityTypeConfiguration());

    }
}

