using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Moq;

using SpaceX.Core.Services.Interfaces;
using SpaceX.WebApi.Controllers;

using ContractRequests = SpaceX.WebApi.Contracts.Requests;
using DomainRequests = SpaceX.Core.Domain.Models.Requests;
using DomainResponses = SpaceX.Core.Domain.Models.Responses;

namespace SpaceX.UnitTests.WebApi.Controllers;

public class LaunchControllerTests
{
    private readonly LaunchController _sut;

    private readonly Mock<ILaunchService> _launchServiceMock = new();

    public LaunchControllerTests()
    {
        _sut = new LaunchController(_launchServiceMock.Object);
    }

    [Fact]
    public async Task GetLatestLaunch_WhenRequestIsSuccessful_ReturnsOkResponse()
    {
        // Arrange
        var response = CreateLaunchResponse();

        _launchServiceMock
            .Setup(x => x.GetLatestLaunchAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _sut.GetLatestLaunch(CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        var objectResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);

        _launchServiceMock.Verify(
            x => x.GetLatestLaunchAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetLatestLaunch_WhenServiceThrowsException_ThrowsException()
    {
        // Arrange
        _launchServiceMock
            .Setup(x => x.GetLatestLaunchAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Get latest launch failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.GetLatestLaunch(CancellationToken.None));

        // Assert
        Assert.Equal("Get latest launch failed.", exception.Message);

        _launchServiceMock.Verify(
            x => x.GetLatestLaunchAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetLaunches_WhenRequestIsSuccessful_ReturnsOkResponse()
    {
        // Arrange
        var request = CreateGetLaunchesRequest();
        var response = CreatePaginatedLaunchesResponse();

        _launchServiceMock
            .Setup(x => x.GetLaunchesAsync(
                It.IsAny<DomainRequests.GetLaunchesRequest>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _sut.GetLaunches(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        var objectResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);

        _launchServiceMock.Verify(
            x => x.GetLaunchesAsync(
                It.IsAny<DomainRequests.GetLaunchesRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetLaunches_WhenServiceThrowsException_ThrowsException()
    {
        // Arrange
        var request = CreateGetLaunchesRequest();

        _launchServiceMock
            .Setup(x => x.GetLaunchesAsync(
                It.IsAny<DomainRequests.GetLaunchesRequest>(),
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Get launches failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.GetLaunches(request, CancellationToken.None));

        // Assert
        Assert.Equal("Get launches failed.", exception.Message);

        _launchServiceMock.Verify(
            x => x.GetLaunchesAsync(
                It.IsAny<DomainRequests.GetLaunchesRequest>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetLaunchDetails_WhenRequestIsSuccessful_ReturnsOkResponse()
    {
        // Arrange
        const string launchId = "launch-id";
        var response = CreateLaunchResponse();

        _launchServiceMock
            .Setup(x => x.GetLaunchDetailsAsync(launchId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _sut.GetLaunchDetails(launchId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        var objectResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);

        _launchServiceMock.Verify(
            x => x.GetLaunchDetailsAsync(launchId, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetLaunchDetails_WhenServiceThrowsException_ThrowsException()
    {
        // Arrange
        const string launchId = "launch-id";

        _launchServiceMock
            .Setup(x => x.GetLaunchDetailsAsync(launchId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Get launch details failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.GetLaunchDetails(launchId, CancellationToken.None));

        // Assert
        Assert.Equal("Get launch details failed.", exception.Message);

        _launchServiceMock.Verify(
            x => x.GetLaunchDetailsAsync(launchId, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetRocketDetails_WhenRequestIsSuccessful_ReturnsOkResponse()
    {
        // Arrange
        const string rocketId = "rocket-id";
        var response = CreateRocketResponse();

        _launchServiceMock
            .Setup(x => x.GetRocketDetailsAsync(rocketId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _sut.GetRocketDetails(rocketId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        var objectResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);

        _launchServiceMock.Verify(
            x => x.GetRocketDetailsAsync(rocketId, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetRocketDetails_WhenServiceThrowsException_ThrowsException()
    {
        // Arrange
        const string rocketId = "rocket-id";

        _launchServiceMock
            .Setup(x => x.GetRocketDetailsAsync(rocketId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Get rocket details failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.GetRocketDetails(rocketId, CancellationToken.None));

        // Assert
        Assert.Equal("Get rocket details failed.", exception.Message);

        _launchServiceMock.Verify(
            x => x.GetRocketDetailsAsync(rocketId, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetLaunchpadDetails_WhenRequestIsSuccessful_ReturnsOkResponse()
    {
        // Arrange
        const string launchpadId = "launchpad-id";
        var response = CreateLaunchpadResponse();

        _launchServiceMock
            .Setup(x => x.GetLaunchpadDetailsAsync(launchpadId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _sut.GetLaunchpadDetails(launchpadId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        var objectResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);

        _launchServiceMock.Verify(
            x => x.GetLaunchpadDetailsAsync(launchpadId, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetLaunchpadDetails_WhenServiceThrowsException_ThrowsException()
    {
        // Arrange
        const string launchpadId = "launchpad-id";

        _launchServiceMock
            .Setup(x => x.GetLaunchpadDetailsAsync(launchpadId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Get launchpad details failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.GetLaunchpadDetails(launchpadId, CancellationToken.None));

        // Assert
        Assert.Equal("Get launchpad details failed.", exception.Message);

        _launchServiceMock.Verify(
            x => x.GetLaunchpadDetailsAsync(launchpadId, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetLandpadDetails_WhenRequestIsSuccessful_ReturnsOkResponse()
    {
        // Arrange
        const string landpadId = "landpad-id";
        var response = CreateLandpadResponse();

        _launchServiceMock
            .Setup(x => x.GetLandpadDetailsAsync(landpadId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _sut.GetLandpadDetails(landpadId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        var objectResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);

        _launchServiceMock.Verify(
            x => x.GetLandpadDetailsAsync(landpadId, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetLandpadDetails_WhenServiceThrowsException_ThrowsException()
    {
        // Arrange
        const string landpadId = "landpad-id";

        _launchServiceMock
            .Setup(x => x.GetLandpadDetailsAsync(landpadId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Get landpad details failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.GetLandpadDetails(landpadId, CancellationToken.None));

        // Assert
        Assert.Equal("Get landpad details failed.", exception.Message);

        _launchServiceMock.Verify(
            x => x.GetLandpadDetailsAsync(landpadId, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetCrewMemberDetails_WhenRequestIsSuccessful_ReturnsOkResponse()
    {
        // Arrange
        const string crewMemberId = "crew-member-id";
        var response = CreateCrewMemberResponse();

        _launchServiceMock
            .Setup(x => x.GetCrewMemberDetailsAsync(crewMemberId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _sut.GetCrewMemberDetails(crewMemberId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        var objectResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);

        _launchServiceMock.Verify(
            x => x.GetCrewMemberDetailsAsync(crewMemberId, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetCrewMemberDetails_WhenServiceThrowsException_ThrowsException()
    {
        // Arrange
        const string crewMemberId = "crew-member-id";

        _launchServiceMock
            .Setup(x => x.GetCrewMemberDetailsAsync(crewMemberId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Get crew member details failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.GetCrewMemberDetails(crewMemberId, CancellationToken.None));

        // Assert
        Assert.Equal("Get crew member details failed.", exception.Message);

        _launchServiceMock.Verify(
            x => x.GetCrewMemberDetailsAsync(crewMemberId, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetCapsuleDetails_WhenRequestIsSuccessful_ReturnsOkResponse()
    {
        // Arrange
        const string capsuleId = "capsule-id";
        var response = CreateCapsuleResponse();

        _launchServiceMock
            .Setup(x => x.GetCapsuleDetailsAsync(capsuleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _sut.GetCapsuleDetails(capsuleId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        var objectResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);

        _launchServiceMock.Verify(
            x => x.GetCapsuleDetailsAsync(capsuleId, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetCapsuleDetails_WhenServiceThrowsException_ThrowsException()
    {
        // Arrange
        const string capsuleId = "capsule-id";

        _launchServiceMock
            .Setup(x => x.GetCapsuleDetailsAsync(capsuleId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Get capsule details failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.GetCapsuleDetails(capsuleId, CancellationToken.None));

        // Assert
        Assert.Equal("Get capsule details failed.", exception.Message);

        _launchServiceMock.Verify(
            x => x.GetCapsuleDetailsAsync(capsuleId, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetShipDetails_WhenRequestIsSuccessful_ReturnsOkResponse()
    {
        // Arrange
        const string shipId = "ship-id";
        var response = CreateShipResponse();

        _launchServiceMock
            .Setup(x => x.GetShipDetailsAsync(shipId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        // Act
        var result = await _sut.GetShipDetails(shipId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);

        var objectResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);

        _launchServiceMock.Verify(
            x => x.GetShipDetailsAsync(shipId, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetShipDetails_WhenServiceThrowsException_ThrowsException()
    {
        // Arrange
        const string shipId = "ship-id";

        _launchServiceMock
            .Setup(x => x.GetShipDetailsAsync(shipId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Get ship details failed."));

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => _sut.GetShipDetails(shipId, CancellationToken.None));

        // Assert
        Assert.Equal("Get ship details failed.", exception.Message);

        _launchServiceMock.Verify(
            x => x.GetShipDetailsAsync(shipId, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    private static ContractRequests.GetLaunchesRequest CreateGetLaunchesRequest()
    {
        return new ContractRequests.GetLaunchesRequest
        {
            Upcoming = true,
            Page = 1,
            Limit = 10
        };
    }
    private static DomainResponses.PaginatedLaunchesResponse CreatePaginatedLaunchesResponse()
    {
        return new DomainResponses.PaginatedLaunchesResponse
        {
            Launches = new List<DomainResponses.LaunchResponse>
            {
                CreateLaunchResponse()
            },
            TotalLaunches = 1,
            Page = 1,
            Limit = 10,
            TotalPages = 1
        };
    }

    private static DomainResponses.LaunchResponse CreateLaunchResponse()
    {
        return new DomainResponses.LaunchResponse
        {
            Id = "launch-id",
            Name = "Falcon 9",
            Details = "Launch details",
            Success = true,
            Upcoming = false,
            FlightNumber = 1,
            DateUtc = DateTime.UtcNow,
            Links = CreateLinksResponse(),
            Rocket = "rocket-id",
            Launchpad = "launchpad-id",
            Crew = new List<DomainResponses.CrewResponse>(),
            Ships = new List<string> { "ship-id" },
            Capsules = new List<string>() { "capsule-id" },
            Cores = new List<DomainResponses.CoreResponse>()
        };
    }

    private static DomainResponses.RocketResponse CreateRocketResponse()
    {
        return new DomainResponses.RocketResponse
        {
            Id = "rocket-id",
            Name = "Falcon 9",
            Type = "rocket",
            CostPerLaunch = 50000000,
            SuccessRatePct = 98,
            Description = "Rocket description",
            FlickrImages = new List<string> { "https://example.com/image.png" }
        };
    }

    private static DomainResponses.LaunchpadResponse CreateLaunchpadResponse()
    {
        return new DomainResponses.LaunchpadResponse
        {
            Id = "launchpad-id",
            Name = "Launchpad",
            FullName = "Launchpad full name",
            Locality = "Cape Canaveral",
            Region = "Florida",
            Latitude = 28.5618571,
            Longitude = -80.577366,
            Status = "active",
            Details = "Launchpad details",
            Images = CreatePadImagesResponse()
        };
    }

    private static DomainResponses.LandpadResponse CreateLandpadResponse()
    {
        return new DomainResponses.LandpadResponse
        {
            Id = "landpad-id",
            Name = "Landpad",
            FullName = "Landpad full name",
            Locality = "Cape Canaveral",
            Type = "Landpad type",
            Region = "Florida",
            Latitude = 28.485833,
            Longitude = -80.544444,
            Status = "active",
            Details = "Landpad details",
            Images = CreatePadImagesResponse()
        };
    }

    private static DomainResponses.PadImagesResponse CreatePadImagesResponse()
    {
        return new DomainResponses.PadImagesResponse
        {
            Large = new List<string> { "https://example.com/image.png" }
        };
    }

    private static DomainResponses.CrewMemberResponse CreateCrewMemberResponse()
    {
        return new DomainResponses.CrewMemberResponse
        {
            Id = "crew-member-id",
            Name = "Crew Member",
            Agency = "NASA",
            Image = "https://example.com/image.png",
            Wikipedia = "https://example.com/wiki",
            Status = "active"
        };
    }

    private static DomainResponses.CapsuleResponse CreateCapsuleResponse()
    {
        return new DomainResponses.CapsuleResponse
        {
            Id = "capsule-id",
            Serial = "C101",
            Status = "active",
            Type = "Dragon 1.0",
            ReuseCount = 1,
            WaterLandings = 1,
            LandLandings = 0,
            LastUpdate = "Last update"
        };
    }

    private static DomainResponses.ShipResponse CreateShipResponse()
    {
        return new DomainResponses.ShipResponse
        {
            Id = "ship-id",
            Name = "Ship",
            Type = "Cargo",
            Roles = new List<string> { "Support Ship" },
            Active = true,
            HomePort = "Port",
            Image = "https://example.com/ship.png"
        };
    }

    private static DomainResponses.LinksResponse CreateLinksResponse()
    {
        return new DomainResponses.LinksResponse
        {
            Patch = CreatePatchResponse(),
            Reddit = CreateRedditResponse(),
            Flickr = CreateFlickrResponse(),
            Presskit = "https://example.com/presskit",
            Webcast = "https://example.com/webcast",
            YoutubeId = "youtube-id",
            Article = "https://example.com/article",
            Wikipedia = "https://example.com/wiki"
        };
    }

    private static DomainResponses.PatchResponse CreatePatchResponse()
    {
        return new DomainResponses.PatchResponse
        {
            Small = "https://example.com/patch-small.png",
            Large = "https://example.com/patch-large.png"
        };
    }

    private static DomainResponses.RedditResponse CreateRedditResponse()
    {
        return new DomainResponses.RedditResponse
        {
            Campaign = "https://example.com/campaign",
            Launch = "https://example.com/launch",
            Media = "https://example.com/media",
            Recovery = "https://example.com/recovery"
        };
    }

    private static DomainResponses.FlickrResponse CreateFlickrResponse()
    {
        return new DomainResponses.FlickrResponse
        {
            Small = new List<string>(),
            Original = new List<string>()
        };
    }
}