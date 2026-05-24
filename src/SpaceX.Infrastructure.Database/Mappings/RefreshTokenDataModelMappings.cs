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
            ExpiresAtUtc = domain.ExpiresAtUtc
        };
    }
}

