using SpaceX.Core.Domain.Models.Requests;

namespace SpaceX.WebApi.Mappings;

public static class AccountMappings
{
    public static CreateAccountRequest ToDomain(this Contracts.Requests.CreateAccountRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new CreateAccountRequest
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password
        };
    }
}

