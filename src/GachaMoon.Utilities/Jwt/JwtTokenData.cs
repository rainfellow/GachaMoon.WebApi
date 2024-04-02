namespace GachaMoon.Utilities.Jwt;

public class JwtTokenData(long userId, long accountId, bool isAdmin, bool isInternalUser)
{
    public long UserId { get; private set; } = userId;
    public long AccountId { get; private set; } = accountId;
    public bool IsAdmin { get; private set; } = isAdmin;
    public bool IsInternalUser { get; private set; } = isInternalUser;
}
