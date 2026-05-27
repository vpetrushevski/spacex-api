using SpaceX.WebApi.Mappings;

using DomainRequests = SpaceX.Core.Domain.Models.Requests;
using DomainResponses = SpaceX.Core.Domain.Models.Responses;
using ContractRequests = SpaceX.WebApi.Contracts.Requests;
using ContractResponses = SpaceX.WebApi.Contracts.Responses;

namespace SpaceX.UnitTests.WebApi.Mappings;

public class AccountMappingsTests
{
    [Fact]
    public void ToDomain_WhenRequestIsValid_ReturnsCreateAccountRequest()
    {
        // Arrange
        var request = CreateContractCreateAccountRequest();

        // Act
        var result = request.ToDomain();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DomainRequests.CreateAccountRequest>(result);
        Assert.Equal(request.FirstName, result.FirstName);
        Assert.Equal(request.LastName, result.LastName);
        Assert.Equal(request.Email, result.Email);
        Assert.Equal(request.Password, result.Password);
    }

    [Fact]
    public void ToDomain_WhenRequestIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractRequests.CreateAccountRequest? request = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => request!.ToDomain());

        // Assert
        Assert.Equal("request", exception.ParamName);
    }

    [Fact]
    public void ToContract_WhenResponseIsValid_ReturnsAccountResponse()
    {
        // Arrange
        var response = CreateDomainAccountResponse();

        // Act
        var result = response.ToContract();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ContractResponses.AccountResponse>(result);
        Assert.Equal(response.Id, result.Id);
        Assert.Equal(response.FirstName, result.FirstName);
        Assert.Equal(response.LastName, result.LastName);
        Assert.Equal(response.Email, result.Email);
    }

    [Fact]
    public void ToContract_WhenResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DomainResponses.AccountResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToContract());

        // Assert
        Assert.Equal("response", exception.ParamName);
    }

    private static ContractRequests.CreateAccountRequest CreateContractCreateAccountRequest()
    {
        return new ContractRequests.CreateAccountRequest
        {
            FirstName = "Vlatko",
            LastName = "Petrushevski",
            Email = "test@test.com",
            Password = "Password123!"
        };
    }

    private static DomainResponses.AccountResponse CreateDomainAccountResponse()
    {
        return new DomainResponses.AccountResponse
        {
            Id = Guid.NewGuid(),
            FirstName = "Vlatko",
            LastName = "Petrushevski",
            Email = "test@test.com"
        };
    }
}