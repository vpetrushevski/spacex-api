using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SpaceX.Infrastructure.Database.Models;

namespace SpaceX.Infrastructure.Database.Configuration;

[ExcludeFromCodeCoverage]
public class RefreshTokenEntityTypeConfiguration : IEntityTypeConfiguration<RefreshTokenDataModel>
{
    public void Configure(EntityTypeBuilder<RefreshTokenDataModel> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("RefreshTokens");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.AccountId).IsRequired();
        builder.Property(x => x.Token).IsRequired().HasMaxLength(128);

        builder.HasOne(d => d.Account)
            .WithMany()
            .HasForeignKey(d => d.AccountId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_RefreshToken_Account");
    }
}
