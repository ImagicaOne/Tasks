using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Server.Models;

namespace Server.Extensions;

public static class AuthenticationHelpers
{
    public static void ProvideSaltAndHash(this UserProfile userProfile)
    {
        var salt = GenerateSalt();
        userProfile.Salt = Convert.ToBase64String(salt);
        userProfile.PasswordHash = ComputeHash(userProfile.PasswordHash, userProfile.Salt);
    }

    public static string? ComputeHash(string? password, string saltString)
    {
        var salt = Convert.FromBase64String(saltString);
        using var hashGenerator = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
        var bytes = hashGenerator.GetBytes(24);
        return Convert.ToBase64String(bytes);
    }

    private static byte[] GenerateSalt()
    {
        var randomNumber = RandomNumberGenerator.Create();
        var salt = new byte[24];
        randomNumber.GetBytes(salt);
        return salt;
    }
}