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

    public static PasswordResetToken ToDomain(this PasswordResetTokenDataModel dataModel)
    {
        ArgumentNullException.ThrowIfNull(dataModel);

        return new PasswordResetToken
        {
            Id = dataModel.Id,
            AccountId = dataModel.AccountId,
            Token = dataModel.Token,
            ExpiresAtUtc = dataModel.ExpiresAtUtc
        };
    }
}
