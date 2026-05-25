using SpaceX.Core.Domain.Entities;
using SpaceX.Infrastructure.Database.Models;

namespace SpaceX.Infrastructure.Database.Mappings;

public static class RefreshTokenDataModelMappings
{
    public static RefreshTokenDataModel ToDataModel(this RefreshToken domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new RefreshTokenDataModel
        {
            AccountId = domain.AccountId,
            Token = domain.Token,
            ExpiresAtUtc = domain.ExpiresAtUtc,
            CreatedAtUtc = DateTimeOffset.UtcNow
        };
    }

    public static RefreshToken ToDomain(this RefreshTokenDataModel dataModel)
    {
        ArgumentNullException.ThrowIfNull(dataModel);

        return new RefreshToken
        {
            Id = dataModel.Id,
            AccountId = dataModel.AccountId,
            Token = dataModel.Token,
            ExpiresAtUtc = dataModel.ExpiresAtUtc,
            Account = dataModel.Account.ToDomain()
        };
    }
}

