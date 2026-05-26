namespace SpaceX.WebApi.Contracts.Responses;

public sealed record FailureResponse
{
    public int? Time { get; init; }

    public int? Altitude { get; init; }

    public string? Reason { get; init; }
}

