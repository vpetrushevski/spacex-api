using SpaceX.Core.Domain.Models.Requests;

namespace SpaceX.Core.Services.Interfaces;

public interface IAccountService
{
    Task CreateAccountAsync(CreateAccountRequest request, CancellationToken cancellationToken = default);

    Task<bool> CheckIsEmailRegisteredAsync(string email, CancellationToken cancellationToken = default);
}
