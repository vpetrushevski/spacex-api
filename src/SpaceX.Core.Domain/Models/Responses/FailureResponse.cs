namespace SpaceX.Core.Domain.Models.Responses;

public class FailureResponse
{
    public int? Time { get; init; }

    public int? Altitude { get; init; }

    public string? Reason { get; init; }
}

