using SpaceX.Core.Domain.Entities;
using SpaceX.Infrastructure.Database.Models;

namespace SpaceX.Infrastructure.Database.Mappings;

public static class AccountDataModelMappings
{
    public static Account ToDomain(this AccountDataModel dataModel)
    {
        ArgumentNullException.ThrowIfNull(dataModel);

        return new Account
        {
            Id = dataModel.Id,
            FirstName = dataModel.FirstName,
            LastName = dataModel.LastName,
            Email = dataModel.Email,
            Password = dataModel.Password,
            Status = dataModel.Status,
            IsVerified = dataModel.IsVerified,
            VerificationToken = dataModel.VerificationToken,
            CreatedAtUtc = dataModel.CreatedAtUtc
        };
    }

    public static AccountDataModel ToDataModel(this Account domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new AccountDataModel
        {
            Id = domain.Id,
            FirstName = domain.FirstName,
            LastName = domain.LastName,
            Email = domain.Email,
            Password = domain.Password,
            Status = domain.Status,
            IsVerified = domain.IsVerified,
            VerificationToken = domain.VerificationToken,
            CreatedAtUtc = domain.CreatedAtUtc
        };
    }
}

