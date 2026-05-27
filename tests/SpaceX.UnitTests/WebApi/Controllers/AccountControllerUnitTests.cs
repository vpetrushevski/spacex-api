using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Moq;

using SpaceX.Core.Services.Interfaces;
using SpaceX.WebApi.Contracts.Requests;
using SpaceX.WebApi.Controllers;

using DomainRequests = SpaceX.Core.Domain.Models.Requests;

namespace SpaceX.UnitTests.WebApi.Controllers;

public class AccountControllerTests
{
    private readonly AccountController _sut;

    private readonly Mock<IAccountService> _accountServiceMock = new();

    public AccountControllerTests()
    {
        _sut = new AccountController(_accountServiceMock.Object);
    }

    [Fact]
    public async Task CreateAccount_WhenRequestIsValid_ReturnsCreatedStatusCode()
    {
        // Arrange
        var request = CreateAccountRequest();

        _accountServiceMock
            .Setup(x => x.CreateAccountAsync(
                It.IsAny<DomainRequests.CreateAccountRequest>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _sut.CreateAccount(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(StatusCodes.Status201Created, statusCodeResult.StatusCode);

        _accountServiceMock.Verify(
            x => x.CreateAccountAsync(
                It.IsAny<DomainRequests.CreateAccountRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task CreateAccount_WhenServiceThrowsException_ThrowsException()
    {
        // Arrange
        var request = CreateAccountRequest();

        _accountServiceMock
            .Setup(x => x.CreateAccountAsync(
                It.IsAny<DomainRequests.CreateAccountRequest>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Create account failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.CreateAccount(request, CancellationToken.None));

        // Assert
        Assert.Equal("Create account failed.", exception.Message);

        _accountServiceMock.Verify(
            x => x.CreateAccountAsync(
                It.IsAny<DomainRequests.CreateAccountRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    private static CreateAccountRequest CreateAccountRequest()
    {
        return new CreateAccountRequest
        {
            FirstName = "Vlatko",
            LastName = "Petrushevski",
            Email = "test@test.com",
            Password = "Password123!"
        };
    }
}