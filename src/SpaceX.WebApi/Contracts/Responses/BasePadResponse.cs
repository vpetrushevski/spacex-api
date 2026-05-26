namespace SpaceX.WebApi.Contracts.Responses;

public record BasePadResponse
{
    public required PadImagesResponse Images { get; init; }

    public required string Name { get; init; }

    public required string FullName { get; init; }

    public required string Status { get; init; }

    public required string Locality { get; init; }

    public required string Region { get; init; }

    public double Latitude { get; init; }

    public double Longitude { get; init; }

    public required string Details { get; init; }

    public required string Id { get; init; }
}