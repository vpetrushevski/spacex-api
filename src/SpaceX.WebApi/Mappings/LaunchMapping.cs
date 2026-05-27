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
            Launches = domain.Launches.Select(x => x.ToContract()).ToList(),
            TotalLaunches = domain.TotalLaunches,
            Limit = domain.Limit,
            TotalPages = domain.TotalPages,
            Page = domain.Page
        };
    }

    public static ContractResponses.LaunchResponse ToContract(this LaunchResponse domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractResponses.LaunchResponse
        {
            Links = domain.Links.ToContract(),
            Rocket = domain.Rocket,
            Success = domain.Success,
            Details = domain.Details,
            Crew = domain.Crew.Select(x => x.ToContract()).ToList(),
            Ships = domain.Ships,
            Capsules = domain.Capsules,
            Launchpad = domain.Launchpad,
            FlightNumber = domain.FlightNumber,
            Name = domain.Name,
            DateUtc = domain.DateUtc,
            Upcoming = domain.Upcoming,
            Cores = domain.Cores.Select(x => x.ToContract()).ToList(),
            Id = domain.Id
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
            LandingType = domain.LandingType,
            Landpad = domain.Landpad
        };
    }

    public static ContractResponses.RocketResponse ToContract(this RocketResponse domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractResponses.RocketResponse
        {
            FlickrImages = domain.FlickrImages,
            Name = domain.Name,
            Type = domain.Type,
            CostPerLaunch = domain.CostPerLaunch,
            SuccessRatePct = domain.SuccessRatePct,
            Description = domain.Description,
            Id = domain.Id
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
            Details = domain.Details,
            Id = domain.Id
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
            Details = domain.Details,
            Id = domain.Id,
            Type = domain.Type
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
            Type = domain.Type,
            Roles = domain.Roles,
            HomePort = domain.HomePort,
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

