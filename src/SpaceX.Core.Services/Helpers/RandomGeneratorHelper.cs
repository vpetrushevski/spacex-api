using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace SpaceX.Core.Services.Helpers;

[ExcludeFromCodeCoverage]
public static class RandomGeneratorHelper
{
    private const string Characters =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public static string RandomString(int length)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(length);

        var result = new char[length];
        var buffer = new byte[length];

        RandomNumberGenerator.Fill(buffer);

        for (var i = 0; i < length; i++)
        {
            result[i] = Characters[buffer[i] % Characters.Length];
        }

        return new string(result);
    }


    public static string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(32);

        return Convert.ToBase64String(randomBytes);
    }
}

