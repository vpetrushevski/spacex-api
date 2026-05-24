using SpaceX.Core.Domain.Models.Requests;

namespace SpaceX.Core.Services.Interfaces;

public interface IAccountService
{
    Task CreateAccountAsync(CreateAccountRequest request);
}
