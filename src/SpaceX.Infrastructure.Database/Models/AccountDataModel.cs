using SpaceX.Core.Domain.Entities.Enums;

namespace SpaceX.Infrastructure.Database.Models;

public class AccountDataModel : BaseDataModel
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public AccountStatus Status { get; set; }

    public bool IsVerified { get; set; }
}