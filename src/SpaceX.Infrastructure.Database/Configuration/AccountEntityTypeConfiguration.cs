using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SpaceX.Infrastructure.Database.Models;

namespace SpaceX.Infrastructure.Database.Configuration;

public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<AccountDataModel>
{
    public void Configure(EntityTypeBuilder<AccountDataModel> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("Accounts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(45);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Password).IsRequired().HasMaxLength(500);

        builder.Property(x => x.Status).IsRequired().HasConversion<int>();

        builder.HasIndex(x => x.Email).IsUnique();
    }
}

