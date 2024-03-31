namespace GachaMoon.Utilities.Jwt;

public class JwtTokenData
{
    public long UserId { get; private set; }
    public long AccountId { get; private set; }
    public bool IsAdmin { get; private set; }
    public bool IsInternalUser { get; private set; }

    public JwtTokenData(long userId, long accountId, bool isAdmin, bool isInternalUser)
    {
        UserId = userId;
        AccountId = accountId;
        IsAdmin = isAdmin;
        IsInternalUser = isInternalUser;
    }
}
