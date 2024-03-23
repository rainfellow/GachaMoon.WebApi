using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GachaMoon.Utilities.Constants;
using Microsoft.IdentityModel.Tokens;

namespace GachaMoon.Utilities.Jwt;

public static class JwtUtilities
{
    public static string GenerateToken(JwtTokenData userData, JwtOptions options)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(options.Key);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GetClaims(userData),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = options.Audience,
            Issuer = options.Issuer
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private static ClaimsIdentity GetClaims(JwtTokenData userData)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, userData.UserId.ToString(CultureInfo.InvariantCulture)),
            new Claim(ClaimTypes.Role, ClaimsIdentity.DefaultRoleClaimType),
            new Claim(UserClaims.IsAdminClaim, userData.IsAdmin.ToString(CultureInfo.InvariantCulture))
        };

        return new ClaimsIdentity(claims);
    }

    public static bool TryGetIdFromToken(string token, JwtOptions options, out long? userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(options.Key);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            userId = long.Parse(
                jwtToken.Claims
                    .First(x => x.Type == JwtRegisteredClaimNames.UniqueName).Value,
                CultureInfo.InvariantCulture);

            return true;
        }
        catch
        {
            userId = null;
            return false;
        }
    }
}
