namespace SpaceX.Core.Domain.Models.Requests;

public class ResetPasswordRequest
{
    public Guid AccountId { get; init; }

    public required string ResetPasswordToken { get; init; }

    public required string NewPassword { get; init; }
}

