using Microsoft.AspNetCore.Mvc.Filters;

using SpaceX.Core.Domain.Models.Authentication;
using SpaceX.Core.Services.Interfaces.Authentication;

using System.Security.Claims;

namespace SpaceX.WebApi.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class AuthRequiredAttribute : ActionFilterAttribute
{
    public bool Optional { get; init; }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var authenticationService = context.HttpContext.RequestServices.GetRequiredService<IAuthenticationService>();
        var currentUserService = context.HttpContext.RequestServices.GetRequiredService<ICurrentUserService>();

        var accessToken = context.HttpContext.Request.Headers.Authorization.ToString();

        if (string.IsNullOrWhiteSpace(accessToken))
        {
            if (Optional)
            {
                currentUserService.SetCurrentUser(null);
                return;
            }

            throw new UnauthorizedAccessException("Missing access token.");
        }

        ClaimsIdentity identity = authenticationService.ValidateAccessToken(accessToken);

        var accountId = GetRequiredClaimValue(identity, ClaimTypes.NameIdentifier);
        var firstName = GetRequiredClaimValue(identity, ClaimTypes.GivenName);
        var lastName = GetRequiredClaimValue(identity, ClaimTypes.Surname);
        var email = GetRequiredClaimValue(identity, ClaimTypes.Email);

        currentUserService.SetCurrentUser(new AuthenticatedUser
        {
            AccountId = Guid.Parse(accountId),
            FirstName = firstName,
            LastName = lastName,
            Email = email
        });
    }

    private static string GetRequiredClaimValue(ClaimsIdentity identity, string claimType)
    {
        var value = identity.FindFirst(claimType)?.Value;

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new UnauthorizedAccessException(
                $"Missing {claimType} claim.");
        }

        return value;
    }
}