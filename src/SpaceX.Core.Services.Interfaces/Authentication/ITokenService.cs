using SpaceX.Core.Domain.Entities;
using SpaceX.Core.Domain.Models.Responses;

namespace SpaceX.Core.Services.Interfaces.Authentication;

public interface ITokenService
{
    Task<LoginResponse> GenerateTokens(Account account, RefreshToken? refreshToken = null);
}

