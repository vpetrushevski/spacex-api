using SpaceX.Infrastructure.Database.Mappings;

using DomainEntities = SpaceX.Core.Domain.Entities;
using DataModels = SpaceX.Infrastructure.Database.Models;

namespace SpaceX.UnitTests.Infrastructure.Database.Mappings;

public class PasswordResetTokenDataModelMappingsTests
{
    [Fact]
    public void ToDataModel_WhenDomainIsValid_ReturnsPasswordResetTokenDataModel()
    {
        // Arrange
        var domain = CreatePasswordResetToken();

        // Act
        var result = domain.ToDataModel();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DataModels.PasswordResetTokenDataModel>(result);
        Assert.Equal(domain.AccountId, result.AccountId);
        Assert.Equal(domain.Token, result.Token);
        Assert.Equal(domain.ExpiresAtUtc, result.ExpiresAtUtc);
        Assert.True(result.CreatedAtUtc <= DateTimeOffset.UtcNow);
    }

    [Fact]
    public void ToDataModel_WhenDomainIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DomainEntities.PasswordResetToken? domain = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => domain!.ToDataModel());

        // Assert
        Assert.Equal("domain", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenDataModelIsValid_ReturnsPasswordResetToken()
    {
        // Arrange
        var dataModel = CreatePasswordResetTokenDataModel();

        // Act
        var result = dataModel.ToDomain();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DomainEntities.PasswordResetToken>(result);
        Assert.Equal(dataModel.Id, result.Id);
        Assert.Equal(dataModel.AccountId, result.AccountId);
        Assert.Equal(dataModel.Token, result.Token);
        Assert.Equal(dataModel.ExpiresAtUtc, result.ExpiresAtUtc);
    }

    [Fact]
    public void ToDomain_WhenDataModelIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DataModels.PasswordResetTokenDataModel? dataModel = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => dataModel!.ToDomain());

        // Assert
        Assert.Equal("dataModel", exception.ParamName);
    }

    private static DomainEntities.PasswordResetToken CreatePasswordResetToken()
    {
        return new DomainEntities.PasswordResetToken
        {
            Id = Guid.NewGuid(),
            AccountId = Guid.NewGuid(),
            Token = "reset-password-token",
            ExpiresAtUtc = DateTimeOffset.UtcNow.AddHours(1)
        };
    }

    private static DataModels.PasswordResetTokenDataModel CreatePasswordResetTokenDataModel()
    {
        return new DataModels.PasswordResetTokenDataModel
        {
            Id = Guid.NewGuid(),
            AccountId = Guid.NewGuid(),
            Token = "reset-password-token",
            ExpiresAtUtc = DateTimeOffset.UtcNow.AddHours(1),
            CreatedAtUtc = DateTimeOffset.UtcNow
        };
    }
}