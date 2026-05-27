using Microsoft.Extensions.DependencyInjection;

using Shouldly;

using SpaceX.Core.Domain.Models.Requests;
using SpaceX.Infrastructure.Interfaces.ExternalApis.SpaceX;
using SpaceX.IntegrationTests.TestEnvironment;

namespace SpaceX.IntegrationTests.ExternalApis.SpaceX;

public class SpaceXClientIntegrationTests : IClassFixture<TestFixture>
{
    private readonly ISpaceXApiClient _client;

    public SpaceXClientIntegrationTests(TestFixture fixture)
    {
        _client = fixture.ServiceProvider.GetRequiredService<ISpaceXApiClient>();
    }

    [Fact]
    public async Task GetLatestLaunchAsync_ShouldReturnLatestLaunch_WhenRequestIsSuccessful()
    {
        // Arrange
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));

        // Act
        var result = await _client.GetLatestLaunchAsync(cancellationTokenSource.Token);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldNotBeNullOrWhiteSpace();
        result.Name.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task GetUpcomingLaunchesAsync_ShouldReturnLaunches_WhenRequestIsSuccessful()
    {
        // Arrange
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));

        var request = new GetLaunchesRequest
        {
            Upcoming = true,
            Page = 1,
            Limit = 10
        };

        // Act
        var result = await _client.GetLaunchesAsync(request, cancellationTokenSource.Token);

        // Assert
        result.ShouldNotBeNull();
        result.Launches.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetPastLaunchesAsync_ShouldReturnLaunches_WhenRequestIsSuccessful()
    {
        // Arrange
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));

        var request = new GetLaunchesRequest
        {
            Upcoming = false,
            Page = 1,
            Limit = 10
        };

        // Act
        var result = await _client.GetLaunchesAsync(request, cancellationTokenSource.Token);

        // Assert
        result.ShouldNotBeNull();
        result.Launches.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetLaunchDetailsAsync_ShouldReturnLaunch_WhenLaunchExists()
    {
        // Arrange
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        const string launchId = "5eb87d42ffd86e000604b384";

        // Act
        var result = await _client.GetLaunchDetailsAsync(launchId, cancellationTokenSource.Token);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(launchId);
        result.Name.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task GetLaunchDetailsAsync_ShouldThrowHttpRequestException_WhenLaunchDoesNotExist()
    {
        // Arrange
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        const string launchId = "000000000000000000000000";

        // Act
        var exception = await Should.ThrowAsync<HttpRequestException>(
            () => _client.GetLaunchDetailsAsync(launchId, cancellationTokenSource.Token));

        // Assert
        exception.Message.ShouldContain("404");
    }

    [Fact]
    public async Task GetRocketDetailsAsync_ShouldReturnRocket_WhenRocketExists()
    {
        // Arrange
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        const string rocketId = "5e9d0d95eda69955f709d1eb";

        // Act
        var result = await _client.GetRocketDetailsAsync(rocketId, cancellationTokenSource.Token);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(rocketId);
        result.Name.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task GetRocketDetailsAsync_ShouldThrowHttpRequestException_WhenRocketDoesNotExist()
    {
        // Arrange
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        const string rocketId = "000000000000000000000000";

        // Act
        var exception = await Should.ThrowAsync<HttpRequestException>(
            () => _client.GetRocketDetailsAsync(rocketId, cancellationTokenSource.Token));

        // Assert
        exception.Message.ShouldContain("404");
    }

    [Fact]
    public async Task GetLaunchpadDetailsAsync_ShouldReturnLaunchpad_WhenLaunchpadExists()
    {
        // Arrange
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        const string launchpadId = "5e9e4502f5090995de566f86";

        // Act
        var result = await _client.GetLaunchpadDetailsAsync(launchpadId, cancellationTokenSource.Token);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(launchpadId);
        result.Name.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task GetLaunchpadDetailsAsync_ShouldThrowHttpRequestException_WhenLaunchpadDoesNotExist()
    {
        // Arrange
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        const string launchpadId = "000000000000000000000000";

        // Act
        var exception = await Should.ThrowAsync<HttpRequestException>(
            () => _client.GetLaunchpadDetailsAsync(launchpadId, cancellationTokenSource.Token));

        // Assert
        exception.Message.ShouldContain("404");
    }

    [Fact]
    public async Task GetLandpadDetailsAsync_ShouldReturnLandpad_WhenLandpadExists()
    {
        // Arrange
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        const string landpadId = "5e9e3032383ecb267a34e7c7";

        // Act
        var result = await _client.GetLandpadDetailsAsync(landpadId, cancellationTokenSource.Token);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(landpadId);
        result.Name.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task GetLandpadDetailsAsync_ShouldThrowHttpRequestException_WhenLandpadDoesNotExist()
    {
        // Arrange
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        const string landpadId = "000000000000000000000000";

        // Act
        var exception = await Should.ThrowAsync<HttpRequestException>(
            () => _client.GetLandpadDetailsAsync(landpadId, cancellationTokenSource.Token));

        // Assert
        exception.Message.ShouldContain("404");
    }

    [Fact]
    public async Task GetCrewMemberDetailsAsync_ShouldReturnCrewMember_WhenCrewMemberExists()
    {
        // Arrange
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        const string crewMemberId = "5ebf1a6e23a9a60006e03a7a";

        // Act
        var result = await _client.GetCrewMemberDetailsAsync(crewMemberId, cancellationTokenSource.Token);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(crewMemberId);
        result.Name.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task GetCrewMemberDetailsAsync_ShouldThrowHttpRequestException_WhenCrewMemberDoesNotExist()
    {
        // Arrange
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        const string crewMemberId = "000000000000000000000000";

        // Act
        var exception = await Should.ThrowAsync<HttpRequestException>(
            () => _client.GetCrewMemberDetailsAsync(crewMemberId, cancellationTokenSource.Token));

        // Assert
        exception.Message.ShouldContain("404");
    }

    [Fact]
    public async Task GetCapsuleDetailsAsync_ShouldReturnCapsule_WhenCapsuleExists()
    {
        // Arrange
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        const string capsuleId = "5e9e2c5bf35918ed873b2664";

        // Act
        var result = await _client.GetCapsuleDetailsAsync(capsuleId, cancellationTokenSource.Token);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(capsuleId);
    }

    [Fact]
    public async Task GetCapsuleDetailsAsync_ShouldThrowHttpRequestException_WhenCapsuleDoesNotExist()
    {
        // Arrange
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        const string capsuleId = "000000000000000000000000";

        // Act
        var exception = await Should.ThrowAsync<HttpRequestException>(
            () => _client.GetCapsuleDetailsAsync(capsuleId, cancellationTokenSource.Token));

        // Assert
        exception.Message.ShouldContain("404");
    }

    [Fact]
    public async Task GetShipDetailsAsync_ShouldReturnShip_WhenShipExists()
    {
        // Arrange
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        const string shipId = "5ea6ed2d080df4000697c901";

        // Act
        var result = await _client.GetShipDetailsAsync(shipId, cancellationTokenSource.Token);

        // Assert
        result.ShouldNotBeNull();
        result.Id.ShouldBe(shipId);
        result.Name.ShouldNotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task GetShipDetailsAsync_ShouldThrowHttpRequestException_WhenShipDoesNotExist()
    {
        // Arrange
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        const string shipId = "000000000000000000000000";

        // Act
        var exception = await Should.ThrowAsync<HttpRequestException>(
            () => _client.GetShipDetailsAsync(shipId, cancellationTokenSource.Token));

        // Assert
        exception.Message.ShouldContain("404");
    }
}