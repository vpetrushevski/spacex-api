using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace SpaceX.Core.Services.Helpers;

[ExcludeFromCodeCoverage]
public static class SecurityHelper
{
    public static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }

    public static string HashString(string text)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(text);

        var hashBytes = SHA512.HashData(
            Encoding.UTF8.GetBytes(text));

        return Convert.ToBase64String(hashBytes);
    }
}

