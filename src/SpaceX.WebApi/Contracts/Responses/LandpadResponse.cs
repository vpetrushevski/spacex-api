namespace SpaceX.WebApi.Contracts.Responses;

public sealed record LandpadResponse : BasePadResponse
{
    public required string Type { get; init; }
}