//https://medium.com/@imAkash25/hashing-and-salting-passwords-in-c-0ee223f07e20#:~:text=Hashing%20Passwords%3A%20A%20One%2DWay%20Journey&text=In%20C%23%2C%20developers%20often%20use,and%20retrieve%20the%20original%20password.

using System.Security.Cryptography;
using System.Text;

namespace MonsterTradingCardsGame.Networking.Server.Utilities;

public static class PasswordUtilities
{
    private static readonly byte[] Salt = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
    
    public static bool VerifyPassword(string password, string hashedPassword) => HashPassword(password) == hashedPassword;
    
    public static string HashPassword(string password)
    {
        using SHA256 sha256 = SHA256.Create();
        
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] saltedPassword = new byte[passwordBytes.Length + Salt.Length];

        Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
        Buffer.BlockCopy(Salt, 0, saltedPassword, passwordBytes.Length, Salt.Length);

        byte[] hashedBytes = sha256.ComputeHash(saltedPassword);

        byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + Salt.Length];
        Buffer.BlockCopy(Salt, 0, hashedPasswordWithSalt, 0, Salt.Length);
        Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, Salt.Length, hashedBytes.Length);

        return Convert.ToBase64String(hashedPasswordWithSalt);
    }
}