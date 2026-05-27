using SpaceX.Infrastructure.ExternalApis.SpaceX.Mappings;

using ContractResponses = SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;
using DomainRequests = SpaceX.Core.Domain.Models.Requests;
using DomainResponses = SpaceX.Core.Domain.Models.Responses;

namespace SpaceX.UnitTests.Infrastructure.ExternalApis.SpaceX.Mappings;

public class LaunchMappingsTests
{
    [Fact]
    public void ToContract_WhenGetLaunchesRequestIsValid_ReturnsGetLaunchesRequest()
    {
        // Arrange
        var request = CreateDomainGetLaunchesRequest();

        // Act
        var result = request.ToContract();

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Query);
        Assert.NotNull(result.Options);
        Assert.NotNull(result.Options.Sort);
        Assert.Equal(request.Upcoming, result.Query.Upcoming);
        Assert.Equal(request.Page, result.Options.Page);
        Assert.Equal(request.Limit, result.Options.Limit);
        Assert.Equal(request.SortDirection, result.Options.Sort.DateUtc);
    }

    [Fact]
    public void ToContract_WhenGetLaunchesRequestIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        DomainRequests.GetLaunchesRequest? request = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => request!.ToContract());

        // Assert
        Assert.Equal("domain", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenPaginatedLaunchesResponseIsValid_ReturnsPaginatedLaunchesResponse()
    {
        // Arrange
        var response = CreateContractPaginatedLaunchesResponse();

        // Act
        var result = response.ToDomain();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<DomainResponses.PaginatedLaunchesResponse>(result);
        Assert.Equal(response.TotalDocs, result.TotalLaunches);
        Assert.Equal(response.Limit, result.Limit);
        Assert.Equal(response.TotalPages, result.TotalPages);
        Assert.Equal(response.Page, result.Page);
        Assert.Single(result.Launches);
    }

    [Fact]
    public void ToDomain_WhenPaginatedLaunchesResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractResponses.PaginatedLaunchesResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToDomain());

        // Assert
        Assert.Equal("contract", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenLaunchResponseIsValid_ReturnsLaunchResponse()
    {
        // Arrange
        var response = CreateContractLaunchResponse();

        // Act
        var result = response.ToDomain();

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
    public void ToDomain_WhenLaunchResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractResponses.LaunchResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToDomain());

        // Assert
        Assert.Equal("contract", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenLinksResponseIsValid_ReturnsLinksResponse()
    {
        // Arrange
        var response = CreateContractLinksResponse();

        // Act
        var result = response.ToDomain();

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
    public void ToDomain_WhenLinksResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractResponses.LinksResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToDomain());

        // Assert
        Assert.Equal("contract", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenPatchResponseIsValid_ReturnsPatchResponse()
    {
        // Arrange
        var response = CreateContractPatchResponse();

        // Act
        var result = response.ToDomain();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.Small, result.Small);
        Assert.Equal(response.Large, result.Large);
    }

    [Fact]
    public void ToDomain_WhenPatchResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractResponses.PatchResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToDomain());

        // Assert
        Assert.Equal("contract", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenRedditResponseIsValid_ReturnsRedditResponse()
    {
        // Arrange
        var response = CreateContractRedditResponse();

        // Act
        var result = response.ToDomain();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.Campaign, result.Campaign);
        Assert.Equal(response.Launch, result.Launch);
        Assert.Equal(response.Media, result.Media);
        Assert.Equal(response.Recovery, result.Recovery);
    }

    [Fact]
    public void ToDomain_WhenRedditResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractResponses.RedditResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToDomain());

        // Assert
        Assert.Equal("contract", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenFlickrResponseIsValid_ReturnsFlickrResponse()
    {
        // Arrange
        var response = CreateContractFlickrResponse();

        // Act
        var result = response.ToDomain();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.Small, result.Small);
        Assert.Equal(response.Original, result.Original);
    }

    [Fact]
    public void ToDomain_WhenFlickrResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractResponses.FlickrResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToDomain());

        // Assert
        Assert.Equal("contract", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenCrewResponseIsValid_ReturnsCrewResponse()
    {
        // Arrange
        var response = CreateContractCrewResponse();

        // Act
        var result = response.ToDomain();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.Crew, result.CrewId);
        Assert.Equal(response.Role, result.Role);
    }

    [Fact]
    public void ToDomain_WhenCrewResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractResponses.CrewResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToDomain());

        // Assert
        Assert.Equal("contract", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenCoreResponseIsValid_ReturnsCoreResponse()
    {
        // Arrange
        var response = CreateContractCoreResponse();

        // Act
        var result = response.ToDomain();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.LandingType, result.LandingType);
        Assert.Equal(response.Landpad, result.Landpad);
    }

    [Fact]
    public void ToDomain_WhenCoreResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractResponses.CoreResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToDomain());

        // Assert
        Assert.Equal("contract", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenRocketResponseIsValid_ReturnsRocketResponse()
    {
        // Arrange
        var response = CreateContractRocketResponse();

        // Act
        var result = response.ToDomain();

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
    public void ToDomain_WhenRocketResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractResponses.RocketResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToDomain());

        // Assert
        Assert.Equal("contract", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenLaunchpadResponseIsValid_ReturnsLaunchpadResponse()
    {
        // Arrange
        var response = CreateContractLaunchpadResponse();

        // Act
        var result = response.ToDomain();

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Images);
        Assert.Equal(response.Name, result.Name);
        Assert.Equal(response.FullName, result.FullName);
        Assert.Equal(response.Status, result.Status);
        Assert.Equal(response.Locality, result.Locality);
        Assert.Equal(response.Region, result.Region);
        Assert.Equal(response.Latitude, result.Latitude);
        Assert.Equal(response.Longitude, result.Longitude);
        Assert.Equal(response.Details, result.Details);
        Assert.Equal(response.Id, result.Id);
    }

    [Fact]
    public void ToDomain_WhenLaunchpadResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractResponses.LaunchpadResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToDomain());

        // Assert
        Assert.Equal("contract", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenLandpadResponseIsValid_ReturnsLandpadResponse()
    {
        // Arrange
        var response = CreateContractLandpadResponse();

        // Act
        var result = response.ToDomain();

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Images);
        Assert.Equal(response.Name, result.Name);
        Assert.Equal(response.FullName, result.FullName);
        Assert.Equal(response.Status, result.Status);
        Assert.Equal(response.Locality, result.Locality);
        Assert.Equal(response.Region, result.Region);
        Assert.Equal(response.Latitude, result.Latitude);
        Assert.Equal(response.Longitude, result.Longitude);
        Assert.Equal(response.Details, result.Details);
        Assert.Equal(response.Id, result.Id);
        Assert.Equal(response.Type, result.Type);
    }

    [Fact]
    public void ToDomain_WhenLandpadResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractResponses.LandpadResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToDomain());

        // Assert
        Assert.Equal("contract", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenPadImagesResponseIsValid_ReturnsPadImagesResponse()
    {
        // Arrange
        var response = CreateContractPadImagesResponse();

        // Act
        var result = response.ToDomain();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response.Large, result.Large);
    }

    [Fact]
    public void ToDomain_WhenPadImagesResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractResponses.PadImagesResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToDomain());

        // Assert
        Assert.Equal("contract", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenCapsuleResponseIsValid_ReturnsCapsuleResponse()
    {
        // Arrange
        var response = CreateContractCapsuleResponse();

        // Act
        var result = response.ToDomain();

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
    public void ToDomain_WhenCapsuleResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractResponses.CapsuleResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToDomain());

        // Assert
        Assert.Equal("contract", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenShipResponseIsValid_ReturnsShipResponse()
    {
        // Arrange
        var response = CreateContractShipResponse();

        // Act
        var result = response.ToDomain();

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
    public void ToDomain_WhenShipResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractResponses.ShipResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToDomain());

        // Assert
        Assert.Equal("contract", exception.ParamName);
    }

    [Fact]
    public void ToDomain_WhenCrewMemberResponseIsValid_ReturnsCrewMemberResponse()
    {
        // Arrange
        var response = CreateContractCrewMemberResponse();

        // Act
        var result = response.ToDomain();

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
    public void ToDomain_WhenCrewMemberResponseIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        ContractResponses.CrewMemberResponse? response = null;

        // Act
        var exception = Assert.Throws<ArgumentNullException>(() => response!.ToDomain());

        // Assert
        Assert.Equal("contract", exception.ParamName);
    }

    private static DomainRequests.GetLaunchesRequest CreateDomainGetLaunchesRequest()
    {
        return new DomainRequests.GetLaunchesRequest
        {
            Upcoming = false,
            Page = 1,
            Limit = 10,
            SortDirection = "desc"
        };
    }

    private static ContractResponses.PaginatedLaunchesResponse CreateContractPaginatedLaunchesResponse()
    {
        return new ContractResponses.PaginatedLaunchesResponse
        {
            Docs = new List<ContractResponses.LaunchResponse>
            {
                CreateContractLaunchResponse()
            },
            TotalDocs = 1,
            Limit = 10,
            TotalPages = 1,
            Page = 1,
            PagingCounter = 1,
            HasPrevPage = false,
            HasNextPage = false,
            PrevPage = null,
            NextPage = null
        };
    }

    private static ContractResponses.LaunchResponse CreateContractLaunchResponse()
    {
        return new ContractResponses.LaunchResponse
        {
            Fairings = CreateContractFairingsResponse(),
            Links = CreateContractLinksResponse(),
            StaticFireDateUtc = DateTimeOffset.UtcNow,
            StaticFireDateUnix = 1234567890,
            Net = false,
            Window = 120,
            Rocket = "rocket-id",
            Success = true,
            Failures = new List<ContractResponses.FailureResponse>
            {
                CreateContractFailureResponse()
            },
            Details = "Launch details",
            Crew = new List<ContractResponses.CrewResponse>
            {
                CreateContractCrewResponse()
            },
            Ships = new List<string>
            {
                "ship-id"
            },
            Capsules = new List<string>
            {
                "capsule-id"
            },
            Payloads = new List<string>
            {
                "payload-id"
            },
            Launchpad = "launchpad-id",
            FlightNumber = 1,
            Name = "Falcon 9",
            DateUtc = DateTimeOffset.UtcNow,
            DateUnix = 1234567890,
            DateLocal = DateTimeOffset.UtcNow,
            DatePrecision = "hour",
            Upcoming = false,
            Cores = new List<ContractResponses.CoreResponse>
            {
                CreateContractCoreResponse()
            },
            AutoUpdate = true,
            Tbd = false,
            LaunchLibraryId = "launch-library-id",
            Id = "launch-id"
        };
    }

    private static ContractResponses.FairingsResponse CreateContractFairingsResponse()
    {
        return new ContractResponses.FairingsResponse
        {
            Reused = false,
            RecoveryAttempt = true,
            Recovered = true,
            Ships = new List<string>
            {
                "fairing-ship-id"
            }
        };
    }

    private static ContractResponses.FailureResponse CreateContractFailureResponse()
    {
        return new ContractResponses.FailureResponse
        {
            Time = 10,
            Altitude = 100,
            Reason = "Failure reason"
        };
    }

    private static ContractResponses.LinksResponse CreateContractLinksResponse()
    {
        return new ContractResponses.LinksResponse
        {
            Patch = CreateContractPatchResponse(),
            Reddit = CreateContractRedditResponse(),
            Flickr = CreateContractFlickrResponse(),
            Presskit = "https://example.com/presskit",
            Webcast = "https://example.com/webcast",
            YoutubeId = "youtube-id",
            Article = "https://example.com/article",
            Wikipedia = "https://example.com/wiki"
        };
    }

    private static ContractResponses.PatchResponse CreateContractPatchResponse()
    {
        return new ContractResponses.PatchResponse
        {
            Small = "https://example.com/patch-small.png",
            Large = "https://example.com/patch-large.png"
        };
    }

    private static ContractResponses.RedditResponse CreateContractRedditResponse()
    {
        return new ContractResponses.RedditResponse
        {
            Campaign = "https://example.com/campaign",
            Launch = "https://example.com/launch",
            Media = "https://example.com/media",
            Recovery = "https://example.com/recovery"
        };
    }

    private static ContractResponses.FlickrResponse CreateContractFlickrResponse()
    {
        return new ContractResponses.FlickrResponse
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

    private static ContractResponses.CrewResponse CreateContractCrewResponse()
    {
        return new ContractResponses.CrewResponse
        {
            Crew = "crew-id",
            Role = "Commander"
        };
    }

    private static ContractResponses.CoreResponse CreateContractCoreResponse()
    {
        return new ContractResponses.CoreResponse
        {
            Core = "core-id",
            Flight = 1,
            Gridfins = true,
            Legs = true,
            Reused = false,
            LandingAttempt = true,
            LandingSuccess = true,
            LandingType = "RTLS",
            Landpad = "landpad-id"
        };
    }

    private static ContractResponses.RocketResponse CreateContractRocketResponse()
    {
        return new ContractResponses.RocketResponse
        {
            Height = CreateContractRocketDimensionResponse(),
            Diameter = CreateContractRocketDimensionResponse(),
            Mass = CreateContractRocketMassResponse(),
            FlickrImages = new List<string>
            {
                "https://example.com/rocket.png"
            },
            Name = "Falcon 9",
            Type = "rocket",
            Active = true,
            Stages = 2,
            Boosters = 0,
            CostPerLaunch = 50000000,
            SuccessRatePct = 98,
            FirstFlight = "2010-06-04",
            Country = "United States",
            Company = "SpaceX",
            Wikipedia = "https://example.com/rocket-wiki",
            Description = "Rocket description",
            Id = "rocket-id"
        };
    }

    private static ContractResponses.RocketDimensionResponse CreateContractRocketDimensionResponse()
    {
        return new ContractResponses.RocketDimensionResponse
        {
            Meters = 70,
            Feet = 229.6
        };
    }

    private static ContractResponses.RocketMassResponse CreateContractRocketMassResponse()
    {
        return new ContractResponses.RocketMassResponse
        {
            Kg = 549054,
            Lb = 1207920
        };
    }

    private static ContractResponses.LaunchpadResponse CreateContractLaunchpadResponse()
    {
        return new ContractResponses.LaunchpadResponse
        {
            Images = CreateContractPadImagesResponse(),
            Name = "Launchpad",
            FullName = "Launchpad full name",
            Status = "active",
            Locality = "Cape Canaveral",
            Region = "Florida",
            Latitude = 28.5618571,
            Longitude = -80.577366,
            Details = "Launchpad details",
            Id = "launchpad-id",
            LaunchAttempts = 10,
            LaunchSuccesses = 9,
            Rockets = new List<string>
            {
                "rocket-id"
            },
            Timezone = "America/New_York"
        };
    }

    private static ContractResponses.LandpadResponse CreateContractLandpadResponse()
    {
        return new ContractResponses.LandpadResponse
        {
            Images = CreateContractPadImagesResponse(),
            Name = "Landpad",
            FullName = "Landpad full name",
            Status = "active",
            Locality = "Cape Canaveral",
            Region = "Florida",
            Latitude = 28.485833,
            Longitude = -80.544444,
            Details = "Landpad details",
            Id = "landpad-id",
            Type = "ASDS",
            LandingAttempts = 10,
            LandingSuccesses = 8,
            Wikipedia = "https://example.com/landpad-wiki"
        };
    }

    private static ContractResponses.PadImagesResponse CreateContractPadImagesResponse()
    {
        return new ContractResponses.PadImagesResponse
        {
            Large = new List<string>
            {
                "https://example.com/pad-large.png"
            }
        };
    }

    private static ContractResponses.CapsuleResponse CreateContractCapsuleResponse()
    {
        return new ContractResponses.CapsuleResponse
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

    private static ContractResponses.ShipResponse CreateContractShipResponse()
    {
        return new ContractResponses.ShipResponse
        {
            LegacyId = "legacy-id",
            Model = "model",
            Type = "Cargo",
            Roles = new List<string>
            {
                "Support Ship"
            },
            YearBuilt = 2010,
            HomePort = "Port Canaveral",
            Status = "active",
            Latitude = 28.0,
            Longitude = -80.0,
            Link = "https://example.com/ship",
            Image = "https://example.com/ship.png",
            Name = "Ship",
            Active = true,
            Id = "ship-id"
        };
    }

    private static ContractResponses.CrewMemberResponse CreateContractCrewMemberResponse()
    {
        return new ContractResponses.CrewMemberResponse
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