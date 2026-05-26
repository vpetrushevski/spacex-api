using System.Text.Json.Serialization;

namespace SpaceX.Infrastructure.ExternalApis.SpaceX.Contracts.Responses;

public abstract class BasePadResponse
{
    [JsonPropertyName("images")]
    public required PadImagesResponse Images { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("full_name")]
    public required string FullName { get; set; }

    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("locality")]
    public required string Locality { get; set; }

    [JsonPropertyName("region")]
    public required string Region { get; set; }

    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("details")]
    public required string Details { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }
}