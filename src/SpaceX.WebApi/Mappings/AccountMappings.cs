using SpaceX.Core.Domain.Models.Requests;
using SpaceX.Core.Domain.Models.Responses;

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

    public static Contracts.Responses.AccountResponse ToContract(this AccountResponse response)
    {
        ArgumentNullException.ThrowIfNull(response);

        return new Contracts.Responses.AccountResponse
        {
            Id = response.Id,
            FirstName = response.FirstName,
            LastName = response.LastName,
            Email = response.Email
        };
    }
}

