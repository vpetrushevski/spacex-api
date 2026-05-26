namespace SpaceX.WebApi.Contracts.Responses;

public sealed record RocketMassResponse
{
    public int Kg { get; init; }

    public int Lb { get; init; }
}