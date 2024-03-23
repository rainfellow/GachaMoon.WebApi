using System.Security.Cryptography;
using System.Text;

namespace GachaMoon.Utilities;

public static class PasswordUtilities
{
    private const int SaltLength = 16;

    public static byte[] GeneratePasswordBytes(string password)
    {
        byte[] salt;
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt = new byte[SaltLength]);

        var hash = GetSHA512FromPasswordAndSalt(password, salt);
        var hashBytes = new byte[salt.Length + hash.Length];

        Array.Copy(salt, 0, hashBytes, 0, salt.Length);
        Array.Copy(hash, 0, hashBytes, salt.Length, hash.Length);

        return hashBytes;
    }

    public static bool VerifyPassword(string password, byte[] passwordBytes)
    {
        var salt = new byte[SaltLength];
        Array.Copy(passwordBytes, 0, salt, 0, salt.Length);

        var hash = GetSHA512FromPasswordAndSalt(password, salt);

        return hash.SequenceEqual(passwordBytes.Skip(salt.Length));
    }

    private static byte[] GetSHA512FromPasswordAndSalt(string password, byte[] salt)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var hashData = new byte[passwordBytes.Length + salt.Length];

        Array.Copy(salt, 0, hashData, 0, salt.Length);
        Array.Copy(passwordBytes, 0, hashData, salt.Length, passwordBytes.Length);

        var hash = SHA512.HashData(hashData);
        return hash;
    }
}
