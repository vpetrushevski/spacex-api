using System.ComponentModel.DataAnnotations;

using SpaceX.Core.Domain.Entities;
using SpaceX.Core.Domain.Entities.Enums;
using SpaceX.Core.Domain.Models.Email;
using SpaceX.Core.Domain.Models.Requests;
using SpaceX.Core.Services.Helpers;
using SpaceX.Core.Services.Interfaces;
using SpaceX.Infrastructure.Interfaces.Database.Repositories;
using SpaceX.Infrastructure.Interfaces.Email;

namespace SpaceX.Core.Services.Accounts;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmailBackgroundDispatcher _emailBackgroundDispatcher;
    private readonly EncryptionHelper _encryptionHelper;

    public AccountService(
        IAccountRepository accountRepository,
        IEmailBackgroundDispatcher emailBackgroundDispatcher,
        EncryptionHelper encryptionHelper)
    {
        _accountRepository = accountRepository;
        _emailBackgroundDispatcher = emailBackgroundDispatcher;
        _encryptionHelper = encryptionHelper;
    }

    public async Task CreateAccountAsync(CreateAccountRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var normalizedEmail = NormalizeEmail(request.Email);
        var encryptedEmail = _encryptionHelper.Encrypt(normalizedEmail);

        var existingAccount = await _accountRepository.GetAccountByEmailAsync(encryptedEmail);

        if (existingAccount is not null)
        {
            throw new ValidationException("Email is already registered to other account.");
        }

        var verificationToken = RandomGeneratorHelper.GenerateRefreshToken();

        var account = new Account()
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = encryptedEmail,
            Password = SecurityHelper.HashPassword(request.Password),
            Status = AccountStatus.AwaitingConfirmation,
            IsVerified = false,
            VerificationToken = SecurityHelper.HashString(verificationToken),
            CreatedAtUtc = DateTimeOffset.UtcNow
        };

        await _accountRepository.CreateAccountAsync(account);

        await _emailBackgroundDispatcher.EnqueueAsync(new EmailMessage
        {
            Type = EmailType.Verification,
            Email = normalizedEmail,
            FirstName = account.FirstName,
            LastName = account.LastName,
            AccountId = account.Id,
            Token = verificationToken
        });
    }

    public async Task<bool> CheckIsEmailRegisteredAsync(string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);

        var normalizedEmail = NormalizeEmail(email);
        var encryptedEmail = _encryptionHelper.Encrypt(normalizedEmail);

        var account = await _accountRepository.GetAccountByEmailAsync(encryptedEmail);

        return account is not null;
    }

    private static string NormalizeEmail(string email)
    {
        return email.Trim().ToLowerInvariant();
    }
}

