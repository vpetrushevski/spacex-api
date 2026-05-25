using SpaceX.Core.Domain.Models.Authentication;

namespace SpaceX.Core.Services.Interfaces.Authentication;

public interface ICurrentUserService
{
    void SetCurrentUser(AuthenticatedUser? user);

    AuthenticatedUser? GetCurrentUser();
}

