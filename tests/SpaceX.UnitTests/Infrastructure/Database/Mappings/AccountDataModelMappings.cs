using SpaceX.Infrastructure.Database.Mappings;

using DomainEntities = SpaceX.Core.Domain.Entities;
using DataModels = SpaceX.Infrastructure.Database.Models;
using SpaceX.Core.Domain.Entities.Enums;

namespace SpaceX.UnitTests.Infrastructure.Database.Mappings;

public class AccountDataModelMappingsTests
{
    [Fact]
    public void ToDomain_WhenDataModelIsValid_ReturnsAccount()
    {
        // Arrange
        var dataModel = CreateAccountDataModel();

        // Act
        var result = dataModel.ToDomain();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DomainEntities.Account>(result);
        Assert.Equal(dataModel.Id, result.Id);
        Assert.Equal(dataModel.FirstName, result.FirstName);
        Assert.Equal(dataModel.LastName, result.LastName);
        Assert.Equal(dataModel.Email, result.Email);
        Assert.Equal(dataModel.Password, result.Password);
        Assert.Equal(dataModel.Status, result.Status);
        Assert.Equal(dataModel.IsVerified, result.IsVerified);
        Assert.Equal(dataModel.VerificationToken, result.VerificationToken);
        Assert.Equal(dataModel.CreatedAtUtc, result.CreatedAtUtc);
    }

    [Fact]
    public void ToDomain_WhenDataModelIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DataModels.AccountDataModel? dataModel = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => dataModel!.ToDomain());

        // Assert
        Assert.Equal("dataModel", exception.ParamName);
    }

    [Fact]
    public void ToDataModel_WhenDomainIsValid_ReturnsAccountDataModel()
    {
        // Arrange
        var domain = CreateAccount();

        // Act
        var result = domain.ToDataModel();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DataModels.AccountDataModel>(result);
        Assert.Equal(domain.Id, result.Id);
        Assert.Equal(domain.FirstName, result.FirstName);
        Assert.Equal(domain.LastName, result.LastName);
        Assert.Equal(domain.Email, result.Email);
        Assert.Equal(domain.Password, result.Password);
        Assert.Equal(domain.Status, result.Status);
        Assert.Equal(domain.IsVerified, result.IsVerified);
        Assert.Equal(domain.VerificationToken, result.VerificationToken);
        Assert.Equal(domain.CreatedAtUtc, result.CreatedAtUtc);
    }

    [Fact]
    public void ToDataModel_WhenDomainIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DomainEntities.Account? domain = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => domain!.ToDataModel());

        // Assert
        Assert.Equal("domain", exception.ParamName);
    }

    private static DataModels.AccountDataModel CreateAccountDataModel()
    {
        return new DataModels.AccountDataModel
        {
            Id = Guid.NewGuid(),
            FirstName = "Vlatko",
            LastName = "Petrushevski",
            Email = "test@test.com",
            Password = "Password123!",
            Status = AccountStatus.Active,
            IsVerified = true,
            VerificationToken = "verification-token",
            CreatedAtUtc = DateTime.UtcNow
        };
    }

    private static DomainEntities.Account CreateAccount()
    {
        return new DomainEntities.Account
        {
            Id = Guid.NewGuid(),
            FirstName = "Vlatko",
            LastName = "Petrushevski",
            Email = "test@test.com",
            Password = "Password123!",
            Status = AccountStatus.Active,
            IsVerified = true,
            VerificationToken = "verification-token",
            CreatedAtUtc = DateTime.UtcNow
        };
    }
}