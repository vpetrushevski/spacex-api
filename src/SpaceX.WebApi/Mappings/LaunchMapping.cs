using SpaceX.Core.Domain.Models.Requests;
using SpaceX.Core.Domain.Models.Responses;

using ContractRequests = SpaceX.WebApi.Contracts.Requests;
using ContractResponses = SpaceX.WebApi.Contracts.Responses;

namespace SpaceX.WebApi.Mappings;

public static class LaunchMapping
{
    public static GetLaunchesRequest ToDomain(this ContractRequests.GetLaunchesRequest contract)
    {
        ArgumentNullException.ThrowIfNull(contract);

        return new GetLaunchesRequest
        {
            Upcoming = contract.Upcoming,
            Page = contract.Page,
            Limit = contract.Limit,
            SortDirection = contract.SortDirection
        };
    }

    public static ContractResponses.PaginatedLaunchesResponse ToContract(this PaginatedLaunchesResponse domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractResponses.PaginatedLaunchesResponse
        {
            Docs = domain.Docs.Select(x => x.ToContract()).ToList(),
            TotalDocs = domain.TotalDocs,
            Limit = domain.Limit,
            TotalPages = domain.TotalPages,
            Page = domain.Page,
            PagingCounter = domain.PagingCounter,
            HasPrevPage = domain.HasPrevPage,
            HasNextPage = domain.HasNextPage,
            PrevPage = domain.PrevPage,
            NextPage = domain.NextPage
        };
    }

    public static ContractResponses.LaunchResponse ToContract(this LaunchResponse domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractResponses.LaunchResponse
        {
            Fairings = domain.Fairings?.ToContract(),
            Links = domain.Links.ToContract(),
            StaticFireDateUtc = domain.StaticFireDateUtc,
            StaticFireDateUnix = domain.StaticFireDateUnix,
            Net = domain.Net,
            Window = domain.Window,
            Rocket = domain.Rocket,
            Success = domain.Success,
            Failures = domain.Failures.Select(x => x.ToContract()).ToList(),
            Details = domain.Details,
            Crew = domain.Crew.Select(x => x.ToContract()).ToList(),
            Ships = domain.Ships,
            Capsules = domain.Capsules,
            Payloads = domain.Payloads,
            Launchpad = domain.Launchpad,
            FlightNumber = domain.FlightNumber,
            Name = domain.Name,
            DateUtc = domain.DateUtc,
            DateUnix = domain.DateUnix,
            DateLocal = domain.DateLocal,
            DatePrecision = domain.DatePrecision,
            Upcoming = domain.Upcoming,
            Cores = domain.Cores.Select(x => x.ToContract()).ToList(),
            AutoUpdate = domain.AutoUpdate,
            Tbd = domain.Tbd,
            LaunchLibraryId = domain.LaunchLibraryId,
            Id = domain.Id
        };
    }

    public static ContractResponses.FairingsResponse ToContract(this FairingsResponse domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractResponses.FairingsResponse
        {
            Reused = domain.Reused,
            RecoveryAttempt = domain.RecoveryAttempt,
            Recovered = domain.Recovered,
            Ships = domain.Ships
        };
    }

    public static ContractResponses.LinksResponse ToContract(this LinksResponse domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractResponses.LinksResponse
        {
            Patch = domain.Patch.ToContract(),
            Reddit = domain.Reddit.ToContract(),
            Flickr = domain.Flickr.ToContract(),
            Presskit = domain.Presskit,
            Webcast = domain.Webcast,
            YoutubeId = domain.YoutubeId,
            Article = domain.Article,
            Wikipedia = domain.Wikipedia
        };
    }

    public static ContractResponses.PatchResponse ToContract(this PatchResponse domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractResponses.PatchResponse
        {
            Small = domain.Small,
            Large = domain.Large
        };
    }

    public static ContractResponses.RedditResponse ToContract(this RedditResponse domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractResponses.RedditResponse
        {
            Campaign = domain.Campaign,
            Launch = domain.Launch,
            Media = domain.Media,
            Recovery = domain.Recovery
        };
    }

    public static ContractResponses.FlickrResponse ToContract(this FlickrResponse domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractResponses.FlickrResponse
        {
            Small = domain.Small,
            Original = domain.Original
        };
    }

    public static ContractResponses.FailureResponse ToContract(this FailureResponse domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractResponses.FailureResponse
        {
            Time = domain.Time,
            Altitude = domain.Altitude,
            Reason = domain.Reason
        };
    }

    public static ContractResponses.CrewResponse ToContract(this CrewResponse domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractResponses.CrewResponse
        {
            CrewId = domain.CrewId,
            Role = domain.Role
        };
    }

    public static ContractResponses.CoreResponse ToContract(this CoreResponse domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractResponses.CoreResponse
        {
            CoreId = domain.CoreId,
            Flight = domain.Flight,
            Gridfins = domain.Gridfins,
            Legs = domain.Legs,
            Reused = domain.Reused,
            LandingAttempt = domain.LandingAttempt,
            LandingSuccess = domain.LandingSuccess,
            LandingType = domain.LandingType,
            Landpad = domain.Landpad
        };
    }

    public static ContractResponses.RocketResponse ToContract(this RocketResponse domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractResponses.RocketResponse
        {
            Height = domain.Height.ToContract(),
            Diameter = domain.Diameter.ToContract(),
            Mass = domain.Mass.ToContract(),
            FlickrImages = domain.FlickrImages,
            Name = domain.Name,
            Type = domain.Type,
            Active = domain.Active,
            Stages = domain.Stages,
            Boosters = domain.Boosters,
            CostPerLaunch = domain.CostPerLaunch,
            SuccessRatePct = domain.SuccessRatePct,
            FirstFlight = domain.FirstFlight,
            Country = domain.Country,
            Company = domain.Company,
            Wikipedia = domain.Wikipedia,
            Description = domain.Description,
            Id = domain.Id
        };
    }

    public static ContractResponses.RocketDimensionResponse ToContract(this RocketDimensionResponse domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractResponses.RocketDimensionResponse
        {
            Meters = domain.Meters,
            Feet = domain.Feet
        };
    }

    public static ContractResponses.RocketMassResponse ToContract(this RocketMassResponse domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractResponses.RocketMassResponse
        {
            Kg = domain.Kg,
            Lb = domain.Lb
        };
    }

    public static ContractResponses.LaunchpadResponse ToContract(this LaunchpadResponse domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractResponses.LaunchpadResponse
        {
            Images = domain.Images.ToContract(),
            Name = domain.Name,
            FullName = domain.FullName,
            Status = domain.Status,
            Locality = domain.Locality,
            Region = domain.Region,
            Latitude = domain.Latitude,
            Longitude = domain.Longitude,
            Details = domain.Details,
            Id = domain.Id,
            LaunchAttempts = domain.LaunchAttempts,
            LaunchSuccesses = domain.LaunchSuccesses,
            Rockets = domain.Rockets,
            Timezone = domain.Timezone
        };
    }

    public static ContractResponses.LandpadResponse ToContract(this LandpadResponse domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractResponses.LandpadResponse
        {
            Images = domain.Images.ToContract(),
            Name = domain.Name,
            FullName = domain.FullName,
            Status = domain.Status,
            Locality = domain.Locality,
            Region = domain.Region,
            Latitude = domain.Latitude,
            Longitude = domain.Longitude,
            Details = domain.Details,
            Id = domain.Id,
            Type = domain.Type,
            LandingAttempts = domain.LandingAttempts,
            LandingSuccesses = domain.LandingSuccesses,
            Wikipedia = domain.Wikipedia
        };
    }

    public static ContractResponses.PadImagesResponse ToContract(this PadImagesResponse domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractResponses.PadImagesResponse
        {
            Large = domain.Large
        };
    }

    public static ContractResponses.CapsuleResponse ToContract(this CapsuleResponse domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractResponses.CapsuleResponse
        {
            ReuseCount = domain.ReuseCount,
            WaterLandings = domain.WaterLandings,
            LandLandings = domain.LandLandings,
            LastUpdate = domain.LastUpdate,
            Serial = domain.Serial,
            Status = domain.Status,
            Type = domain.Type,
            Id = domain.Id
        };
    }

    public static ContractResponses.ShipResponse ToContract(this ShipResponse domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractResponses.ShipResponse
        {
            LegacyId = domain.LegacyId,
            Model = domain.Model,
            Type = domain.Type,
            Roles = domain.Roles,
            YearBuilt = domain.YearBuilt,
            HomePort = domain.HomePort,
            Status = domain.Status,
            Latitude = domain.Latitude,
            Longitude = domain.Longitude,
            Link = domain.Link,
            Image = domain.Image,
            Name = domain.Name,
            Active = domain.Active,
            Id = domain.Id
        };
    }

    public static ContractResponses.CrewMemberResponse ToContract(this CrewMemberResponse domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractResponses.CrewMemberResponse
        {
            Name = domain.Name,
            Agency = domain.Agency,
            Image = domain.Image,
            Wikipedia = domain.Wikipedia,
            Status = domain.Status,
            Id = domain.Id
        };
    }
}

