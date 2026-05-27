namespace SpaceX.Core.Domain.Models.Responses;

public class LandpadResponse : BasePadResponse
{
    public required string Type { get; init; }
}