using SpaceX.WebApi.Mappings;

using ContractRequests = SpaceX.WebApi.Contracts.Requests;
using ContractResponses = SpaceX.WebApi.Contracts.Responses;
using DomainRequests = SpaceX.Core.Domain.Models.Requests;
using DomainResponses = SpaceX.Core.Domain.Models.Responses;

namespace SpaceX.UnitTests.WebApi.Mappings;

public class LaunchMappingTests
{
    [Fact]
    public void ToDomain_WhenGetLaunchesRequestIsValid_ReturnsGetLaunchesRequest()
    {
        // Arrange
        var request = CreateContractGetLaunchesRequest();

        // Act
        var result = request.ToDomain();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DomainRequests.GetLaunchesRequest>(result);
        Assert.Equal(request.Upcoming, result.Upcoming);
        Assert.Equal(request.Page, result.Page);
        Assert.Equal(request.Limit, result.Limit);
        Assert.Equal(request.SortDirection, result.SortDirection);
    }

    [Fact]
    public void ToDomain_WhenGetLaunchesRequestIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractRequests.GetLaunchesRequest? request = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => request!.ToDomain());

        // Assert
        Assert.Equal("contract", exception.ParamName);
    }

    [Fact]
    public void ToContract_WhenPaginatedLaunchesResponseIsValid_ReturnsPaginatedLaunchesResponse()
    {
        // Arrange
        var response = CreateDomainPaginatedLaunchesResponse();

        // Act
        var result = response.ToContract();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ContractResponses.PaginatedLaunchesResponse>(result);
        Assert.Equal(response.TotalLaunches, result.TotalLaunches);
        Assert.Equal(response.Limit, result.Limit);
        Assert.Equal(response.TotalPages, result.TotalPages);
        Assert.Equal(response.Page, result.Page);
        Assert.Single(result.Launches);
    }

    [Fact]
    public void ToContract_WhenPaginatedLaunchesResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DomainResponses.PaginatedLaunchesResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToContract());

        // Assert
        Assert.Equal("domain", exception.ParamName);
    }

    [Fact]
    public void ToContract_WhenLaunchResponseIsValid_ReturnsLaunchResponse()
    {
        // Arrange
        var response = CreateDomainLaunchResponse();

        // Act
        var result = response.ToContract();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.Rocket, result.Rocket);
        Assert.Equal(response.Success, result.Success);
        Assert.Equal(response.Details, result.Details);
        Assert.Equal(response.Ships, result.Ships);
        Assert.Equal(response.Capsules, result.Capsules);
        Assert.Equal(response.Launchpad, result.Launchpad);
        Assert.Equal(response.FlightNumber, result.FlightNumber);
        Assert.Equal(response.Name, result.Name);
        Assert.Equal(response.DateUtc, result.DateUtc);
        Assert.Equal(response.Upcoming, result.Upcoming);
        Assert.Equal(response.Id, result.Id);
        Assert.NotNull(result.Links);
        Assert.Single(result.Crew);
        Assert.Single(result.Cores);
    }

    [Fact]
    public void ToContract_WhenLaunchResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DomainResponses.LaunchResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToContract());

        // Assert
        Assert.Equal("domain", exception.ParamName);
    }

    [Fact]
    public void ToContract_WhenLinksResponseIsValid_ReturnsLinksResponse()
    {
        // Arrange
        var response = CreateDomainLinksResponse();

        // Act
        var result = response.ToContract();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.Presskit, result.Presskit);
        Assert.Equal(response.Webcast, result.Webcast);
        Assert.Equal(response.YoutubeId, result.YoutubeId);
        Assert.Equal(response.Article, result.Article);
        Assert.Equal(response.Wikipedia, result.Wikipedia);
        Assert.NotNull(result.Patch);
        Assert.NotNull(result.Reddit);
        Assert.NotNull(result.Flickr);
    }

    [Fact]
    public void ToContract_WhenLinksResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DomainResponses.LinksResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToContract());

        // Assert
        Assert.Equal("domain", exception.ParamName);
    }

    [Fact]
    public void ToContract_WhenPatchResponseIsValid_ReturnsPatchResponse()
    {
        // Arrange
        var response = CreateDomainPatchResponse();

        // Act
        var result = response.ToContract();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.Small, result.Small);
        Assert.Equal(response.Large, result.Large);
    }

    [Fact]
    public void ToContract_WhenPatchResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DomainResponses.PatchResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToContract());

        // Assert
        Assert.Equal("domain", exception.ParamName);
    }

    [Fact]
    public void ToContract_WhenRedditResponseIsValid_ReturnsRedditResponse()
    {
        // Arrange
        var response = CreateDomainRedditResponse();

        // Act
        var result = response.ToContract();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.Campaign, result.Campaign);
        Assert.Equal(response.Launch, result.Launch);
        Assert.Equal(response.Media, result.Media);
        Assert.Equal(response.Recovery, result.Recovery);
    }

    [Fact]
    public void ToContract_WhenRedditResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DomainResponses.RedditResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToContract());

        // Assert
        Assert.Equal("domain", exception.ParamName);
    }

    [Fact]
    public void ToContract_WhenFlickrResponseIsValid_ReturnsFlickrResponse()
    {
        // Arrange
        var response = CreateDomainFlickrResponse();

        // Act
        var result = response.ToContract();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.Small, result.Small);
        Assert.Equal(response.Original, result.Original);
    }

    [Fact]
    public void ToContract_WhenFlickrResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DomainResponses.FlickrResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToContract());

        // Assert
        Assert.Equal("domain", exception.ParamName);
    }

    [Fact]
    public void ToContract_WhenCrewResponseIsValid_ReturnsCrewResponse()
    {
        // Arrange
        var response = CreateDomainCrewResponse();

        // Act
        var result = response.ToContract();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.CrewId, result.CrewId);
        Assert.Equal(response.Role, result.Role);
    }

    [Fact]
    public void ToContract_WhenCrewResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DomainResponses.CrewResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToContract());

        // Assert
        Assert.Equal("domain", exception.ParamName);
    }

    [Fact]
    public void ToContract_WhenCoreResponseIsValid_ReturnsCoreResponse()
    {
        // Arrange
        var response = CreateDomainCoreResponse();

        // Act
        var result = response.ToContract();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.LandingType, result.LandingType);
        Assert.Equal(response.Landpad, result.Landpad);
    }

    [Fact]
    public void ToContract_WhenCoreResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DomainResponses.CoreResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToContract());

        // Assert
        Assert.Equal("domain", exception.ParamName);
    }

    [Fact]
    public void ToContract_WhenRocketResponseIsValid_ReturnsRocketResponse()
    {
        // Arrange
        var response = CreateDomainRocketResponse();

        // Act
        var result = response.ToContract();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.FlickrImages, result.FlickrImages);
        Assert.Equal(response.Name, result.Name);
        Assert.Equal(response.Type, result.Type);
        Assert.Equal(response.CostPerLaunch, result.CostPerLaunch);
        Assert.Equal(response.SuccessRatePct, result.SuccessRatePct);
        Assert.Equal(response.Description, result.Description);
        Assert.Equal(response.Id, result.Id);
    }

    [Fact]
    public void ToContract_WhenRocketResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DomainResponses.RocketResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToContract());

        // Assert
        Assert.Equal("domain", exception.ParamName);
    }

    [Fact]
    public void ToContract_WhenLaunchpadResponseIsValid_ReturnsLaunchpadResponse()
    {
        // Arrange
        var response = CreateDomainLaunchpadResponse();

        // Act
        var result = response.ToContract();

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Images);
        Assert.Equal(response.Name, result.Name);
        Assert.Equal(response.FullName, result.FullName);
        Assert.Equal(response.Status, result.Status);
        Assert.Equal(response.Locality, result.Locality);
        Assert.Equal(response.Region, result.Region);
        Assert.Equal(response.Details, result.Details);
        Assert.Equal(response.Id, result.Id);
    }

    [Fact]
    public void ToContract_WhenLaunchpadResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DomainResponses.LaunchpadResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToContract());

        // Assert
        Assert.Equal("domain", exception.ParamName);
    }

    [Fact]
    public void ToContract_WhenLandpadResponseIsValid_ReturnsLandpadResponse()
    {
        // Arrange
        var response = CreateDomainLandpadResponse();

        // Act
        var result = response.ToContract();

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Images);
        Assert.Equal(response.Name, result.Name);
        Assert.Equal(response.FullName, result.FullName);
        Assert.Equal(response.Status, result.Status);
        Assert.Equal(response.Locality, result.Locality);
        Assert.Equal(response.Region, result.Region);
        Assert.Equal(response.Details, result.Details);
        Assert.Equal(response.Id, result.Id);
        Assert.Equal(response.Type, result.Type);
    }

    [Fact]
    public void ToContract_WhenLandpadResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DomainResponses.LandpadResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToContract());

        // Assert
        Assert.Equal("domain", exception.ParamName);
    }

    [Fact]
    public void ToContract_WhenPadImagesResponseIsValid_ReturnsPadImagesResponse()
    {
        // Arrange
        var response = CreateDomainPadImagesResponse();

        // Act
        var result = response.ToContract();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.Large, result.Large);
    }

    [Fact]
    public void ToContract_WhenPadImagesResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DomainResponses.PadImagesResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToContract());

        // Assert
        Assert.Equal("domain", exception.ParamName);
    }

    [Fact]
    public void ToContract_WhenCapsuleResponseIsValid_ReturnsCapsuleResponse()
    {
        // Arrange
        var response = CreateDomainCapsuleResponse();

        // Act
        var result = response.ToContract();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.ReuseCount, result.ReuseCount);
        Assert.Equal(response.WaterLandings, result.WaterLandings);
        Assert.Equal(response.LandLandings, result.LandLandings);
        Assert.Equal(response.LastUpdate, result.LastUpdate);
        Assert.Equal(response.Serial, result.Serial);
        Assert.Equal(response.Status, result.Status);
        Assert.Equal(response.Type, result.Type);
        Assert.Equal(response.Id, result.Id);
    }

    [Fact]
    public void ToContract_WhenCapsuleResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DomainResponses.CapsuleResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToContract());

        // Assert
        Assert.Equal("domain", exception.ParamName);
    }

    [Fact]
    public void ToContract_WhenShipResponseIsValid_ReturnsShipResponse()
    {
        // Arrange
        var response = CreateDomainShipResponse();

        // Act
        var result = response.ToContract();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.Type, result.Type);
        Assert.Equal(response.Roles, result.Roles);
        Assert.Equal(response.HomePort, result.HomePort);
        Assert.Equal(response.Image, result.Image);
        Assert.Equal(response.Name, result.Name);
        Assert.Equal(response.Active, result.Active);
        Assert.Equal(response.Id, result.Id);
    }

    [Fact]
    public void ToContract_WhenShipResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DomainResponses.ShipResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToContract());

        // Assert
        Assert.Equal("domain", exception.ParamName);
    }

    [Fact]
    public void ToContract_WhenCrewMemberResponseIsValid_ReturnsCrewMemberResponse()
    {
        // Arrange
        var response = CreateDomainCrewMemberResponse();

        // Act
        var result = response.ToContract();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.Name, result.Name);
        Assert.Equal(response.Agency, result.Agency);
        Assert.Equal(response.Image, result.Image);
        Assert.Equal(response.Wikipedia, result.Wikipedia);
        Assert.Equal(response.Status, result.Status);
        Assert.Equal(response.Id, result.Id);
    }

    [Fact]
    public void ToContract_WhenCrewMemberResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DomainResponses.CrewMemberResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToContract());

        // Assert
        Assert.Equal("domain", exception.ParamName);
    }

    private static ContractRequests.GetLaunchesRequest CreateContractGetLaunchesRequest()
    {
        return new ContractRequests.GetLaunchesRequest
        {
            Upcoming = false,
            Page = 1,
            Limit = 10,
            SortDirection = "desc"
        };
    }

    private static DomainResponses.PaginatedLaunchesResponse CreateDomainPaginatedLaunchesResponse()
    {
        return new DomainResponses.PaginatedLaunchesResponse
        {
            Launches = new List<DomainResponses.LaunchResponse>
            {
                CreateDomainLaunchResponse()
            },
            TotalLaunches = 1,
            Limit = 10,
            TotalPages = 1,
            Page = 1
        };
    }

    private static DomainResponses.LaunchResponse CreateDomainLaunchResponse()
    {
        return new DomainResponses.LaunchResponse
        {
            Links = CreateDomainLinksResponse(),
            Rocket = "rocket-id",
            Success = true,
            Details = "Launch details",
            Crew = new List<DomainResponses.CrewResponse>
            {
                CreateDomainCrewResponse()
            },
            Ships = new List<string>
            {
                "ship-id"
            },
            Capsules = new List<string>
            {
                "capsule-id"
            },
            Launchpad = "launchpad-id",
            FlightNumber = 1,
            Name = "Falcon 9",
            DateUtc = DateTime.UtcNow,
            Upcoming = false,
            Cores = new List<DomainResponses.CoreResponse>
            {
                CreateDomainCoreResponse()
            },
            Id = "launch-id"
        };
    }

    private static DomainResponses.LinksResponse CreateDomainLinksResponse()
    {
        return new DomainResponses.LinksResponse
        {
            Patch = CreateDomainPatchResponse(),
            Reddit = CreateDomainRedditResponse(),
            Flickr = CreateDomainFlickrResponse(),
            Presskit = "https://example.com/presskit",
            Webcast = "https://example.com/webcast",
            YoutubeId = "youtube-id",
            Article = "https://example.com/article",
            Wikipedia = "https://example.com/wiki"
        };
    }

    private static DomainResponses.PatchResponse CreateDomainPatchResponse()
    {
        return new DomainResponses.PatchResponse
        {
            Small = "https://example.com/patch-small.png",
            Large = "https://example.com/patch-large.png"
        };
    }

    private static DomainResponses.RedditResponse CreateDomainRedditResponse()
    {
        return new DomainResponses.RedditResponse
        {
            Campaign = "https://example.com/campaign",
            Launch = "https://example.com/launch",
            Media = "https://example.com/media",
            Recovery = "https://example.com/recovery"
        };
    }

    private static DomainResponses.FlickrResponse CreateDomainFlickrResponse()
    {
        return new DomainResponses.FlickrResponse
        {
            Small = new List<string>
            {
                "https://example.com/flickr-small.png"
            },
            Original = new List<string>
            {
                "https://example.com/flickr-original.png"
            }
        };
    }

    private static DomainResponses.CrewResponse CreateDomainCrewResponse()
    {
        return new DomainResponses.CrewResponse
        {
            CrewId = "crew-id",
            Role = "Commander"
        };
    }

    private static DomainResponses.CoreResponse CreateDomainCoreResponse()
    {
        return new DomainResponses.CoreResponse
        {
            LandingType = "RTLS",
            Landpad = "landpad-id"
        };
    }

    private static DomainResponses.RocketResponse CreateDomainRocketResponse()
    {
        return new DomainResponses.RocketResponse
        {
            FlickrImages = new List<string>
            {
                "https://example.com/rocket.png"
            },
            Name = "Falcon 9",
            Type = "rocket",
            CostPerLaunch = 50000000,
            SuccessRatePct = 98,
            Description = "Rocket description",
            Id = "rocket-id"
        };
    }

    private static DomainResponses.LaunchpadResponse CreateDomainLaunchpadResponse()
    {
        return new DomainResponses.LaunchpadResponse
        {
            Images = CreateDomainPadImagesResponse(),
            Name = "Launchpad",
            FullName = "Launchpad full name",
            Status = "active",
            Locality = "Cape Canaveral",
            Region = "Florida",
            Latitude = 28.5618571,
            Longitude = -80.577366,
            Details = "Launchpad details",
            Id = "launchpad-id"
        };
    }

    private static DomainResponses.LandpadResponse CreateDomainLandpadResponse()
    {
        return new DomainResponses.LandpadResponse
        {
            Images = CreateDomainPadImagesResponse(),
            Name = "Landpad",
            FullName = "Landpad full name",
            Status = "active",
            Locality = "Cape Canaveral",
            Region = "Florida",
            Latitude = 28.485833,
            Longitude = -80.544444,
            Details = "Landpad details",
            Id = "landpad-id",
            Type = "ASDS"
        };
    }

    private static DomainResponses.PadImagesResponse CreateDomainPadImagesResponse()
    {
        return new DomainResponses.PadImagesResponse
        {
            Large = new List<string>
            {
                "https://example.com/pad-large.png"
            }
        };
    }

    private static DomainResponses.CapsuleResponse CreateDomainCapsuleResponse()
    {
        return new DomainResponses.CapsuleResponse
        {
            ReuseCount = 1,
            WaterLandings = 1,
            LandLandings = 0,
            LastUpdate = "Last update",
            Serial = "C101",
            Status = "active",
            Type = "Dragon 1.0",
            Id = "capsule-id"
        };
    }

    private static DomainResponses.ShipResponse CreateDomainShipResponse()
    {
        return new DomainResponses.ShipResponse
        {
            Type = "Cargo",
            Roles = new List<string>
            {
                "Support Ship"
            },
            HomePort = "Port Canaveral",
            Image = "https://example.com/ship.png",
            Name = "Ship",
            Active = true,
            Id = "ship-id"
        };
    }

    private static DomainResponses.CrewMemberResponse CreateDomainCrewMemberResponse()
    {
        return new DomainResponses.CrewMemberResponse
        {
            Name = "Vlatko Petrushevski",
            Agency = "NASA",
            Image = "https://example.com/crew.png",
            Wikipedia = "https://example.com/wiki",
            Status = "active",
            Id = "crew-member-id"
        };
    }
}