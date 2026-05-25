using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using SpaceX.Core.Domain.Configuration;
using SpaceX.Core.Domain.Entities;
using SpaceX.Core.Domain.Entities.Enums;
using SpaceX.Core.Domain.Models.Requests;
using SpaceX.Core.Domain.Models.Responses;
using SpaceX.Core.Services.Helpers;
using SpaceX.Core.Services.Interfaces.Authentication;
using SpaceX.Infrastructure.Interfaces.Database.Repositories;

namespace SpaceX.Core.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IAuthenticationRepository _authenticationRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly ITokenService _tokenService;
    private readonly EncryptionHelper _encryptionHelper;
    private readonly JwtTokenConfiguration _jwtTokenConfiguration;

    public AuthenticationService(
        IAccountRepository accountRepository,
        IAuthenticationRepository authenticationRepository,
        ICurrentUserService currentUserService,
        ITokenService tokenService,
        EncryptionHelper encryptionHelper,
        IOptions<JwtTokenConfiguration> jwtTokenConfiguration)
    {
        ArgumentNullException.ThrowIfNull(jwtTokenConfiguration);

        _accountRepository = accountRepository;
        _authenticationRepository = authenticationRepository;
        _currentUserService = currentUserService;
        _tokenService = tokenService;
        _encryptionHelper = encryptionHelper;
        _jwtTokenConfiguration = jwtTokenConfiguration.Value;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var normalizedEmail = NormalizeEmail(request.Email);
        var encryptedEmail = _encryptionHelper.Encrypt(normalizedEmail);

        var account = await _accountRepository.GetAccountByEmailAsync(encryptedEmail)
            ?? throw new ValidationException("Email is not registered yet.");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, account.Password))
        {
            throw new ValidationException("Wrong password.");
        }

        EnsureAccountIsActive(account.Status);

        return await _tokenService.GenerateTokens(account);
    }

    public async Task<LoginResponse> AuthorizeAsync(string accessToken)
    {
        if (string.IsNullOrWhiteSpace(accessToken))
        {
            throw new ValidationException("Access token is missing.");
        }

        var claimsIdentity = ValidateAccessToken(accessToken);
        var accountId = GetAccountIdFromClaims(claimsIdentity);

        var account = await _accountRepository.GetAccountAsync(accountId)
            ?? throw new ValidationException("Access token is invalid.");

        EnsureAccountIsActive(account.Status);

        return await _tokenService.GenerateTokens(account);
    }

    public async Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var claimsPrincipal = GetClaimsFromExpiredToken(request.AccessToken);
        var accountIdValue = GetRequiredClaimValue(claimsPrincipal, ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(accountIdValue, out var accountId))
        {
            throw new ValidationException("Access token is invalid.");
        }

        var hashedRefreshToken = SecurityHelper.HashString(request.RefreshToken);

        var refreshToken = await _authenticationRepository.GetRefreshTokenAsync(accountId, hashedRefreshToken)
            ?? throw new ValidationException("Refresh token is invalid.");

        EnsureAccountIsActive(refreshToken.Account.Status);

        return await _tokenService.GenerateTokens(refreshToken.Account, refreshToken);
    }

    public ClaimsIdentity ValidateAccessToken(string accessToken, bool validateExpirationTime = true)
    {
        if (string.IsNullOrWhiteSpace(accessToken))
        {
            throw new ValidationException("Access token is missing.");
        }

        var jwtToken = accessToken.Replace("Bearer ", string.Empty);

        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = CreateTokenValidationParameters(validateExpirationTime);

        try
        {
            var principal = tokenHandler.ValidateToken(jwtToken, validationParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken)
            {
                throw new ValidationException("Access token is invalid.");
            }

            return (ClaimsIdentity)principal.Identity!;
        }
        catch (SecurityTokenExpiredException)
        {
            throw new ValidationException("Access token is expired.");
        }
        catch (Exception)
        {
            throw new ValidationException("Access token is invalid.");
        }
    }

    public async Task LogoutAsync(LogoutRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var currentUser = _currentUserService.GetCurrentUser()
            ?? throw new ValidationException("Access token is invalid.");

        var hashedRefreshToken = SecurityHelper.HashString(request.RefreshToken);

        var refreshToken = await _authenticationRepository.GetRefreshTokenAsync(currentUser.AccountId, hashedRefreshToken)
            ?? throw new ValidationException("Refresh token is invalid.");

        await _authenticationRepository.RemoveRefreshTokenAsync(refreshToken.AccountId, refreshToken.Token);
    }

    public async Task SendVerificationEmailAsync(string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);

        var normalizedEmail = NormalizeEmail(email);
        var encryptedEmail = _encryptionHelper.Encrypt(normalizedEmail);

        var account = await _accountRepository.GetAccountByEmailAsync(encryptedEmail)
            ?? throw new ValidationException("Email is not registered yet.");

        EnsureAccountIsAwaitingConfirmation(account.Status);

        //TODO: Send verification email
    }

    public async Task VerifyAccountAsync(VerifyAccountRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var account = await _accountRepository.GetAccountAsync(request.AccountId)
            ?? throw new ValidationException("Account does not exist.");

        EnsureAccountIsAwaitingConfirmation(account.Status);

        if (string.IsNullOrWhiteSpace(account.VerificationToken))
        {
            throw new ValidationException("Verification token is missing.");
        }

        var hashedToken = SecurityHelper.HashString(request.Token);

        if (account.VerificationToken != hashedToken)
        {
            throw new ValidationException("Verification token is invalid.");
        }

        account.Status = AccountStatus.Active;
        account.IsVerified = true;
        account.VerificationToken = null;

        await _accountRepository.UpdateAccountAsync(account);
    }

    public async Task SendForgotPasswordEmailAsync(string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);

        var normalizedEmail = NormalizeEmail(email);
        var encryptedEmail = _encryptionHelper.Encrypt(normalizedEmail);

        var account = await _accountRepository.GetAccountByEmailAsync(encryptedEmail)
            ?? throw new ValidationException("Email is not registered yet.");

        EnsureAccountIsActive(account.Status);

        var token = RandomGeneratorHelper.GenerateRefreshToken();
        var hashedToken = SecurityHelper.HashString(token);

        var passwordResetToken = new PasswordResetToken
        {
            AccountId = account.Id,
            Token = hashedToken,
            ExpiresAtUtc = DateTimeOffset.UtcNow.AddDays(1)
        };

        await _authenticationRepository.CreatePasswordResetTokenAsync(passwordResetToken);

        //TODO: Send reset password email
    }

    public async Task ResetPasswordAsync(ResetPasswordRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var account = await _accountRepository.GetAccountAsync(request.AccountId)
            ?? throw new ValidationException("Account does not exist.");

        EnsureAccountIsActive(account.Status);

        var hashedToken = SecurityHelper.HashString(request.ResetPasswordToken);

        var passwordResetToken = await _authenticationRepository.GetPasswordResetTokenByAccountIdAndHashedTokenAsync(account.Id, hashedToken)
            ?? throw new ValidationException("Token is not valid.");

        if (passwordResetToken.ExpiresAtUtc < DateTimeOffset.UtcNow)
        {
            throw new ValidationException("Token is expired.");
        }

        account.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

        await _accountRepository.UpdateAccountAsync(account);

        var tokens = await _authenticationRepository.GetPasswordResetTokensAsync(account.Id);

        if (tokens.Any())
        {
            await _authenticationRepository.DeletePasswordResetTokensAsync(tokens);
        }

        //TODO: Send changed password confirmation email
    }

    public async Task ChangePasswordAsync(ChangePasswordRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var currentUser = _currentUserService.GetCurrentUser()
            ?? throw new ValidationException("Access token is invalid.");

        var account = await _accountRepository.GetAccountAsync(currentUser.AccountId)
            ?? throw new ValidationException("Account does not exist.");

        EnsureAccountIsActive(account.Status);

        if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, account.Password))
        {
            throw new ValidationException("Wrong current password.");
        }

        account.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

        await _accountRepository.UpdateAccountAsync(account);
    }

    private ClaimsPrincipal GetClaimsFromExpiredToken(string expiredAccessToken)
    {
        if (string.IsNullOrWhiteSpace(expiredAccessToken))
        {
            throw new ValidationException("Access token is missing.");
        }

        var token = expiredAccessToken.Replace("Bearer ", string.Empty);
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = CreateTokenValidationParameters(validateExpirationTime: false);

        try
        {
            return tokenHandler.ValidateToken(
                token,
                validationParameters,
                out _);
        }
        catch (Exception)
        {
            throw new ValidationException("Token validation failed.");
        }
    }

    private TokenValidationParameters CreateTokenValidationParameters(bool validateExpirationTime)
    {
        var key = Encoding.UTF8.GetBytes(_jwtTokenConfiguration.Secret);

        return new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),

            ValidateIssuer = true,
            ValidIssuer = _jwtTokenConfiguration.ValidIssuer,

            ValidateAudience = true,
            ValidAudience = _jwtTokenConfiguration.ValidAudience,

            RequireExpirationTime = validateExpirationTime,
            ValidateLifetime = validateExpirationTime,

            ClockSkew = TimeSpan.Zero
        };
    }

    private static Guid GetAccountIdFromClaims(ClaimsIdentity identity)
    {
        var accountIdValue = GetRequiredClaimValue(identity, ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(accountIdValue, out var accountId))
        {
            throw new ValidationException("Access token is invalid.");
        }

        return accountId;
    }

    private static string GetRequiredClaimValue(ClaimsPrincipal principal, string claimType)
    {
        var value = principal.FindFirst(claimType)?.Value;

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ValidationException($"Missing {claimType} claim.");
        }

        return value;
    }

    private static string GetRequiredClaimValue(ClaimsIdentity identity, string claimType)
    {
        var value = identity.FindFirst(claimType)?.Value;

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ValidationException($"Missing {claimType} claim.");
        }

        return value;
    }

    private static string NormalizeEmail(string email)
    {
        return email.Trim().ToLowerInvariant();
    }

    private static void EnsureAccountIsActive(AccountStatus status)
    {
        switch (status)
        {
            case AccountStatus.Active:
                return;

            case AccountStatus.AwaitingConfirmation:
                throw new ValidationException("Account is not verified.");

            case AccountStatus.Blocked:
                throw new ValidationException("Account is blocked.");

            case AccountStatus.Disabled:
                throw new ValidationException("Account is disabled.");

            default:
                throw new ValidationException("Invalid account status.");
        }
    }

    private static void EnsureAccountIsAwaitingConfirmation(AccountStatus status)
    {
        switch (status)
        {
            case AccountStatus.AwaitingConfirmation:
                return;

            case AccountStatus.Active:
                throw new ValidationException("Account is already verified.");

            case AccountStatus.Blocked:
                throw new ValidationException("Account is blocked.");

            case AccountStatus.Disabled:
                throw new ValidationException("Account is disabled.");

            default:
                throw new ValidationException("Invalid account status.");
        }
    }
}