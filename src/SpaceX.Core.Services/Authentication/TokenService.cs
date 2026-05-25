using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using SpaceX.Core.Domain.Configuration;
using SpaceX.Core.Domain.Entities;
using SpaceX.Core.Domain.Models.Responses;
using SpaceX.Core.Services.Helpers;
using SpaceX.Core.Services.Interfaces.Authentication;
using SpaceX.Infrastructure.Interfaces.Database.Repositories;

namespace SpaceX.Core.Services.Authentication;

public class TokenService : ITokenService
{
    private readonly IAuthenticationRepository _authenticationRepository;
    private readonly EncryptionHelper _encryptionHelper;
    private readonly JwtTokenConfiguration _jwtTokenConfiguration;

    public TokenService(
        IAuthenticationRepository authenticationRepository,
        EncryptionHelper encryptionHelper,
        IOptions<JwtTokenConfiguration> jwtTokenConfiguration)
    {
        ArgumentNullException.ThrowIfNull(jwtTokenConfiguration);

        _authenticationRepository = authenticationRepository;
        _encryptionHelper = encryptionHelper;
        _jwtTokenConfiguration = jwtTokenConfiguration.Value;
    }

    public async Task<LoginResponse> GenerateTokens(Account account, RefreshToken? refreshToken = null)
    {
        ArgumentNullException.ThrowIfNull(account);

        var decryptedEmail = _encryptionHelper.Decrypt(account.Email);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, account.Id.ToString()),
            new(ClaimTypes.GivenName, account.FirstName),
            new(ClaimTypes.Surname, account.LastName),
            new(ClaimTypes.Email, decryptedEmail)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenConfiguration.Secret));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtTokenConfiguration.TokenValidityInMinutes),
            Issuer = _jwtTokenConfiguration.ValidIssuer,
            Audience = _jwtTokenConfiguration.ValidAudience,
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var accessToken = tokenHandler.CreateToken(tokenDescriptor);
        var refreshTokenValue = RandomGeneratorHelper.GenerateRefreshToken();

        var refreshTokenRequest = new RefreshToken
        {
            AccountId = account.Id,
            Token = SecurityHelper.HashString(refreshTokenValue),
            ExpiresAtUtc = DateTimeOffset.UtcNow.AddDays(_jwtTokenConfiguration.RefreshTokenValidityInDays)
        };

        await _authenticationRepository.CreateRefreshTokenAsync(refreshTokenRequest);

        if (refreshToken is not null)
        {
            await _authenticationRepository.DeleteRefreshTokenAsync(refreshToken.AccountId, refreshToken.Token);
        }

        return new LoginResponse
        {
            AccessToken = tokenHandler.WriteToken(accessToken),
            RefreshToken = refreshTokenValue,
            Account = new AccountResponse
            {
                Id = account.Id,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = decryptedEmail
            }
        };
    }
}