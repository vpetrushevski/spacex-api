using System.Diagnostics.CodeAnalysis;

using SpaceX.Core.Domain.Models.Authentication;
using SpaceX.Core.Services.Interfaces.Authentication;

namespace SpaceX.Core.Services.Authentication;

[ExcludeFromCodeCoverage]
public class CurrentUserService : ICurrentUserService
{
    private AuthenticatedUser? _currentUser;

    public void SetCurrentUser(AuthenticatedUser? user)
    {
        _currentUser = user;
    }

    public AuthenticatedUser? GetCurrentUser()
    {
        return _currentUser;
    }
}

