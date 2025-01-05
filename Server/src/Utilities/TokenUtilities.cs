using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Server.Utilities;

public static class TokenUtilities
{
    private static readonly byte[] Key =
        "z97ki8G7ueDqICty6OxMHsL29tq3w6rq06FWsJt1o7lLGU4X6qyFvjdvkL1rGLjd"u8.ToArray();

    public static string GenerateJwtToken(string username)
    {
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, username)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static bool ValidateToken(string token, out ClaimsPrincipal? principal)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        principal = null;

        try
        {
            ClaimsPrincipal? claims = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out _);
            principal = claims;
            
            return true;
        }
        catch
        {
            return false;
        }
    }
}