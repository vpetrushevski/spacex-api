using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;

public sealed class PaginatedLaunchesResponse
{
    [JsonPropertyName("docs")]
    public required IReadOnlyList<LaunchResponse> Docs { get; set; }

    [JsonPropertyName("totalDocs")]
    public int TotalDocs { get; set; }

    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("pagingCounter")]
    public int PagingCounter { get; set; }

    [JsonPropertyName("hasPrevPage")]
    public bool HasPrevPage { get; set; }

    [JsonPropertyName("hasNextPage")]
    public bool HasNextPage { get; set; }

    [JsonPropertyName("prevPage")]
    public int? PrevPage { get; set; }

    [JsonPropertyName("nextPage")]
    public int? NextPage { get; set; }
}

