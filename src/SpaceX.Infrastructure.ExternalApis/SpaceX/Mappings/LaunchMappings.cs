using SpaceX.Core.Domain.Models.Requests;
using SpaceX.Core.Domain.Models.Responses;

using ContractRequests = SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Requests;
using ContractResponses = SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Mappings;

public static class LaunchMappings
{
    public static ContractRequests.GetLaunchesRequest ToContract(this GetLaunchesRequest domain)
    {
        ArgumentNullException.ThrowIfNull(domain);

        return new ContractRequests.GetLaunchesRequest
        {
            Query = new ContractRequests.LaunchQueryRequest
            {
                Upcoming = domain.Upcoming
            },
            Options = new ContractRequests.LaunchOptionsRequest
            {
                Page = domain.Page,
                Limit = domain.Limit,
                Sort = new ContractRequests.LaunchSortRequest
                {
                    DateUtc = domain.SortDirection
                }
            }
        };
    }

    public static PaginatedLaunchesResponse ToDomain(this ContractResponses.PaginatedLaunchesResponse contract)
    {
        ArgumentNullException.ThrowIfNull(contract);

        return new PaginatedLaunchesResponse
        {
            Launches = contract.Docs.Select(x => x.ToDomain()).ToList(),
            TotalLaunches = contract.TotalDocs,
            Limit = contract.Limit,
            TotalPages = contract.TotalPages,
            Page = contract.Page
        };
    }

    public static LaunchResponse ToDomain(this ContractResponses.LaunchResponse contract)
    {
        ArgumentNullException.ThrowIfNull(contract);

        return new LaunchResponse
        {
            Links = contract.Links.ToDomain(),
            Rocket = contract.Rocket,
            Success = contract.Success,
            Details = contract.Details,
            Crew = contract.Crew.Select(x => x.ToDomain()).ToList(),
            Ships = contract.Ships,
            Capsules = contract.Capsules,
            Launchpad = contract.Launchpad,
            FlightNumber = contract.FlightNumber,
            Name = contract.Name,
            DateUtc = contract.DateUtc,
            Upcoming = contract.Upcoming,
            Cores = contract.Cores.Select(x => x.ToDomain()).ToList(),
            Id = contract.Id
        };
    }

    public static LinksResponse ToDomain(this ContractResponses.LinksResponse contract)
    {
        ArgumentNullException.ThrowIfNull(contract);

        return new LinksResponse
        {
            Patch = contract.Patch.ToDomain(),
            Reddit = contract.Reddit.ToDomain(),
            Flickr = contract.Flickr.ToDomain(),
            Presskit = contract.Presskit,
            Webcast = contract.Webcast,
            YoutubeId = contract.YoutubeId,
            Article = contract.Article,
            Wikipedia = contract.Wikipedia
        };
    }

    public static PatchResponse ToDomain(this ContractResponses.PatchResponse contract)
    {
        ArgumentNullException.ThrowIfNull(contract);

        return new PatchResponse
        {
            Small = contract.Small,
            Large = contract.Large
        };
    }

    public static RedditResponse ToDomain(this ContractResponses.RedditResponse contract)
    {
        ArgumentNullException.ThrowIfNull(contract);

        return new RedditResponse
        {
            Campaign = contract.Campaign,
            Launch = contract.Launch,
            Media = contract.Media,
            Recovery = contract.Recovery
        };
    }

    public static FlickrResponse ToDomain(this ContractResponses.FlickrResponse contract)
    {
        ArgumentNullException.ThrowIfNull(contract);

        return new FlickrResponse
        {
            Small = contract.Small,
            Original = contract.Original
        };
    }

    public static CrewResponse ToDomain(this ContractResponses.CrewResponse contract)
    {
        ArgumentNullException.ThrowIfNull(contract);

        return new CrewResponse
        {
            CrewId = contract.Crew,
            Role = contract.Role
        };
    }

    public static CoreResponse ToDomain(this ContractResponses.CoreResponse contract)
    {
        ArgumentNullException.ThrowIfNull(contract);

        return new CoreResponse
        {
            LandingType = contract.LandingType,
            Landpad = contract.Landpad
        };
    }

    public static RocketResponse ToDomain(this ContractResponses.RocketResponse contract)
    {
        ArgumentNullException.ThrowIfNull(contract);

        return new RocketResponse
        {
            FlickrImages = contract.FlickrImages,
            Name = contract.Name,
            Type = contract.Type,
            CostPerLaunch = contract.CostPerLaunch,
            SuccessRatePct = contract.SuccessRatePct,
            Description = contract.Description,
            Id = contract.Id
        };
    }

    public static LaunchpadResponse ToDomain(this ContractResponses.LaunchpadResponse contract)
    {
        ArgumentNullException.ThrowIfNull(contract);

        return new LaunchpadResponse
        {
            Images = contract.Images.ToDomain(),
            Name = contract.Name,
            FullName = contract.FullName,
            Status = contract.Status,
            Locality = contract.Locality,
            Region = contract.Region,
            Latitude = contract.Latitude,
            Longitude = contract.Longitude,
            Details = contract.Details,
            Id = contract.Id
        };
    }

    public static LandpadResponse ToDomain(this ContractResponses.LandpadResponse contract)
    {
        ArgumentNullException.ThrowIfNull(contract);

        return new LandpadResponse
        {
            Images = contract.Images.ToDomain(),
            Name = contract.Name,
            FullName = contract.FullName,
            Status = contract.Status,
            Locality = contract.Locality,
            Region = contract.Region,
            Latitude = contract.Latitude,
            Longitude = contract.Longitude,
            Details = contract.Details,
            Id = contract.Id,
            Type = contract.Type
        };
    }

    public static PadImagesResponse ToDomain(this ContractResponses.PadImagesResponse contract)
    {
        ArgumentNullException.ThrowIfNull(contract);

        return new PadImagesResponse
        {
            Large = contract.Large
        };
    }

    public static CapsuleResponse ToDomain(this ContractResponses.CapsuleResponse contract)
    {
        ArgumentNullException.ThrowIfNull(contract);

        return new CapsuleResponse
        {
            ReuseCount = contract.ReuseCount,
            WaterLandings = contract.WaterLandings,
            LandLandings = contract.LandLandings,
            LastUpdate = contract.LastUpdate,
            Serial = contract.Serial,
            Status = contract.Status,
            Type = contract.Type,
            Id = contract.Id
        };
    }

    public static ShipResponse ToDomain(this ContractResponses.ShipResponse contract)
    {
        ArgumentNullException.ThrowIfNull(contract);

        return new ShipResponse
        {
            Type = contract.Type,
            Roles = contract.Roles,
            HomePort = contract.HomePort,
            Image = contract.Image,
            Name = contract.Name,
            Active = contract.Active,
            Id = contract.Id
        };
    }

    public static CrewMemberResponse ToDomain(this ContractResponses.CrewMemberResponse contract)
    {
        ArgumentNullException.ThrowIfNull(contract);

        return new CrewMemberResponse
        {
            Name = contract.Name,
            Agency = contract.Agency,
            Image = contract.Image,
            Wikipedia = contract.Wikipedia,
            Status = contract.Status,
            Id = contract.Id
        };
    }
}

