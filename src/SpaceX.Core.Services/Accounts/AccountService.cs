using System.ComponentModel.DataAnnotations;
using SpaceX.Core.Domain.Entities;
using SpaceX.Core.Domain.Entities.Enums;
using SpaceX.Core.Domain.Models.Requests;
using SpaceX.Core.Services.Helpers;
using SpaceX.Core.Services.Interfaces;
using SpaceX.Infrastructure.Interfaces.Database.Repositories;

namespace SpaceX.Core.Services.Accounts;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    private readonly EncryptionHelper _encryptionHelper;

    public AccountService(
        IAccountRepository accountRepository,
        EncryptionHelper encryptionHelper)
    {
        _accountRepository = accountRepository;
        _encryptionHelper = encryptionHelper;
    }

    public async Task CreateAccountAsync(CreateAccountRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var normalizedEmail = request.Email.Trim().ToLowerInvariant();
        var encryptedEmail = _encryptionHelper.Encrypt(normalizedEmail);

        var existingAccount = await _accountRepository.GetAccountByEmailAsync(encryptedEmail);

        if (existingAccount is not null)
        {
            throw new ValidationException("Email is already registered to other account.");
        }

        var account = new Account()
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            Email = encryptedEmail,
            Password = SecurityHelper.HashPassword(request.Password),
            Status = AccountStatus.AwaitingConfirmation,
            IsVerified = false,
            CreatedAtUtc = DateTimeOffset.UtcNow
        };

        await _accountRepository.CreateAccountAsync(account);
    }
}

