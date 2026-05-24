using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpaceX.Infrastructure.Database.Models;

namespace SpaceX.Infrastructure.Database.Configuration;

public class PasswordResetTokenEntityTypeConfiguration : IEntityTypeConfiguration<PasswordResetTokenDataModel>
{
    public void Configure(EntityTypeBuilder<PasswordResetTokenDataModel> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("PasswordResetTokens");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.AccountId).IsRequired();
        builder.Property(x => x.Token).IsRequired().HasMaxLength(128);

        builder.HasOne(d => d.Account)
            .WithMany()
            .HasForeignKey(d => d.AccountId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_PasswordResetToken_Account");
    }
}