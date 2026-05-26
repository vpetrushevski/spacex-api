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
            Docs = contract.Docs.Select(x => x.ToDomain()).ToList(),
            TotalDocs = contract.TotalDocs,
            Limit = contract.Limit,
            TotalPages = contract.TotalPages,
            Page = contract.Page,
            PagingCounter = contract.PagingCounter,
            HasPrevPage = contract.HasPrevPage,
            HasNextPage = contract.HasNextPage,
            PrevPage = contract.PrevPage,
            NextPage = contract.NextPage
        };
    }

    public static LaunchResponse ToDomain(this ContractResponses.LaunchResponse contract)
    {
        ArgumentNullException.ThrowIfNull(contract);

        return new LaunchResponse
        {
            Fairings = contract.Fairings?.ToDomain(),
            Links = contract.Links.ToDomain(),
            StaticFireDateUtc = contract.StaticFireDateUtc,
            StaticFireDateUnix = contract.StaticFireDateUnix,
            Net = contract.Net,
            Window = contract.Window,
            Rocket = contract.Rocket,
            Success = contract.Success,
            Failures = contract.Failures.Select(x => x.ToDomain()).ToList(),
            Details = contract.Details,
            Crew = contract.Crew.Select(x => x.ToDomain()).ToList(),
            Ships = contract.Ships,
            Capsules = contract.Capsules,
            Payloads = contract.Payloads,
            Launchpad = contract.Launchpad,
            FlightNumber = contract.FlightNumber,
            Name = contract.Name,
            DateUtc = contract.DateUtc,
            DateUnix = contract.DateUnix,
            DateLocal = contract.DateLocal,
            DatePrecision = contract.DatePrecision,
            Upcoming = contract.Upcoming,
            Cores = contract.Cores.Select(x => x.ToDomain()).ToList(),
            AutoUpdate = contract.AutoUpdate,
            Tbd = contract.Tbd,
            LaunchLibraryId = contract.LaunchLibraryId,
            Id = contract.Id
        };
    }

    public static FairingsResponse ToDomain(this ContractResponses.FairingsResponse contract)
    {
        ArgumentNullException.ThrowIfNull(contract);

        return new FairingsResponse
        {
            Reused = contract.Reused,
            RecoveryAttempt = contract.RecoveryAttempt,
            Recovered = contract.Recovered,
            Ships = contract.Ships
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

    public static FailureResponse ToDomain(this ContractResponses.FailureResponse contract)
    {
        ArgumentNullException.ThrowIfNull(contract);

        return new FailureResponse
        {
            Time = contract.Time,
            Altitude = contract.Altitude,
            Reason = contract.Reason
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
            CoreId = contract.Core,
            Flight = contract.Flight,
            Gridfins = contract.Gridfins,
            Legs = contract.Legs,
            Reused = contract.Reused,
            LandingAttempt = contract.LandingAttempt,
            LandingSuccess = contract.LandingSuccess,
            LandingType = contract.LandingType,
            Landpad = contract.Landpad
        };
    }
}

