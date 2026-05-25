namespace SpaceX.WebApi.Contracts.Requests;

public sealed record ResetPasswordRequest
{
    public Guid AccountId { get; init; }

    public required string ResetPasswordToken { get; init; }

    public required string NewPassword { get; init; }
}

