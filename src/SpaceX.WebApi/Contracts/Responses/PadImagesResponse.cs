namespace SpaceX.WebApi.Contracts.Responses;

public sealed record PadImagesResponse
{
    public required IReadOnlyList<string> Large { get; set; }
}