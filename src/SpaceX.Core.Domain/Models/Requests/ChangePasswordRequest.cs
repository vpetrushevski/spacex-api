namespace SpaceX.Core.Domain.Models.Requests;

public class ChangePasswordRequest
{
    public required string CurrentPassword { get; init; }

    public required string NewPassword { get; init; }
}

