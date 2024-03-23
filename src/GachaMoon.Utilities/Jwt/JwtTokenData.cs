namespace GachaMoon.Utilities.Jwt;

public class JwtTokenData
{
    public long UserId { get; private set; }
    public bool IsAdmin { get; private set; }

    public JwtTokenData(long userId, bool isAdmin)
    {
        UserId = userId;
        IsAdmin = isAdmin;
    }
}
