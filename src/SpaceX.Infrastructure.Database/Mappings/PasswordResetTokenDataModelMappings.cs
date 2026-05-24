using SpaceX.Core.Domain.Entities;
using SpaceX.Infrastructure.Database.Models;

namespace SpaceX.Infrastructure.Database.Mappings;

public static class PasswordResetTokenDataModelMappings
{
    public static PasswordResetTokenDataModel ToDataModel(this PasswordResetToken domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new PasswordResetTokenDataModel
        {
            AccountId = domain.AccountId,
            Token = domain.Token,
            ExpiresAtUtc = domain.ExpiresAtUtc
        };
    }
}