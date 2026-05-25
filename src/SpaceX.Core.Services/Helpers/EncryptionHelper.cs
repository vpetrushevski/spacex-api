using System.Security.Cryptography;
using System.Text;

using Microsoft.Extensions.Options;

using SpaceX.Core.Domain.Configuration;

namespace SpaceX.Core.Services.Helpers;

public sealed class EncryptionHelper
{
    private const int AesKeySizeInBytes = 32;
    private const int AesIvSizeInBytes = 16;

    private readonly EncryptionConfiguration _configuration;

    public EncryptionHelper(IOptions<EncryptionConfiguration> configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        _configuration = configuration.Value;
    }

    public string Encrypt(string plainText)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(plainText);

        byte[] key = GetEncryptionKey();

        using var aes = Aes.Create();
        aes.Key = key;
        aes.GenerateIV();
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var memoryStream = new MemoryStream();

        memoryStream.Write(aes.IV, 0, aes.IV.Length);

        using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
        using (var streamWriter = new StreamWriter(cryptoStream, Encoding.UTF8))
        {
            streamWriter.Write(plainText);
        }

        return Convert.ToBase64String(memoryStream.ToArray());
    }

    public string Decrypt(string encryptedText)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(encryptedText);

        byte[] fullCipher = Convert.FromBase64String(encryptedText);

        if (fullCipher.Length <= AesIvSizeInBytes)
        {
            throw new InvalidOperationException("Invalid encrypted text.");
        }

        byte[] iv = fullCipher[..AesIvSizeInBytes];
        byte[] cipherText = fullCipher[AesIvSizeInBytes..];

        byte[] key = GetEncryptionKey();

        using var aes = Aes.Create();
        aes.Key = key;
        aes.IV = iv;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using var memoryStream = new MemoryStream(cipherText);
        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        using var streamReader = new StreamReader(cryptoStream, Encoding.UTF8);

        return streamReader.ReadToEnd();
    }

    private byte[] GetEncryptionKey()
    {
        byte[] key = Convert.FromBase64String(_configuration.EncryptionKey);

        if (key.Length != AesKeySizeInBytes)
        {
            throw new InvalidOperationException("Encryption key must be 32 bytes Base64 string.");
        }

        return key;
    }
}



