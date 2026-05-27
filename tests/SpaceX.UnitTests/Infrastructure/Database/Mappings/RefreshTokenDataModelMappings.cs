using SpaceX.Core.Domain.Entities.Enums;
using SpaceX.Infrastructure.Database.Mappings;

using DataModels = SpaceX.Infrastructure.Database.Models;
using DomainEntities = SpaceX.Core.Domain.Entities;

namespace SpaceX.UnitTests.Infrastructure.Database.Mappings;

public class RefreshTokenDataModelMappingsTests
{
    [Fact]
    public void ToDataModel_WhenDomainIsValid_ReturnsRefreshTokenDataModel()
    {
        // Arrange
        var domain = CreateRefreshToken();

        // Act
        var result = domain.ToDataModel();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DataModels.RefreshTokenDataModel>(result);
        Assert.Equal(domain.AccountId, result.AccountId);
        Assert.Equal(domain.Token, result.Token);
        Assert.Equal(domain.ExpiresAtUtc, result.ExpiresAtUtc);
        Assert.True(result.CreatedAtUtc <= DateTimeOffset.UtcNow);
    }

    [Fact]
    public void ToDataModel_WhenDomainIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DomainEntities.RefreshToken? domain = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => domain!.ToDataModel());

        // Assert
        Assert.Equal("domain", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenDataModelIsValid_ReturnsRefreshToken()
    {
        // Arrange
        var dataModel = CreateRefreshTokenDataModel();

        // Act
        var result = dataModel.ToDomain();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DomainEntities.RefreshToken>(result);
        Assert.Equal(dataModel.Id, result.Id);
        Assert.Equal(dataModel.AccountId, result.AccountId);
        Assert.Equal(dataModel.Token, result.Token);
        Assert.Equal(dataModel.ExpiresAtUtc, result.ExpiresAtUtc);
        Assert.NotNull(result.Account);
        Assert.Equal(dataModel.Account.Id, result.Account.Id);
        Assert.Equal(dataModel.Account.FirstName, result.Account.FirstName);
        Assert.Equal(dataModel.Account.LastName, result.Account.LastName);
        Assert.Equal(dataModel.Account.Email, result.Account.Email);
    }

    [Fact]
    public void ToDomain_WhenDataModelIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DataModels.RefreshTokenDataModel? dataModel = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => dataModel!.ToDomain());

        // Assert
        Assert.Equal("dataModel", exception.ParamName);
    }

    private static DomainEntities.RefreshToken CreateRefreshToken()
    {
        return new DomainEntities.RefreshToken
        {
            Id = Guid.NewGuid(),
            AccountId = Guid.NewGuid(),
            Token = "refresh-token",
            ExpiresAtUtc = DateTimeOffset.UtcNow.AddDays(7)
        };
    }

    private static DataModels.RefreshTokenDataModel CreateRefreshTokenDataModel()
    {
        return new DataModels.RefreshTokenDataModel
        {
            Id = Guid.NewGuid(),
            AccountId = Guid.NewGuid(),
            Token = "refresh-token",
            ExpiresAtUtc = DateTimeOffset.UtcNow.AddDays(7),
            CreatedAtUtc = DateTimeOffset.UtcNow,
            Account = CreateAccountDataModel()
        };
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
            CreatedAtUtc = DateTimeOffset.UtcNow
        };
    }
}