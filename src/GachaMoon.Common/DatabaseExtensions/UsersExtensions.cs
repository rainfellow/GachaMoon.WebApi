using GachaMoon.Common.Query;
using GachaMoon.Domain.Users;
using GachaMoon.Utilities.Formatted;

namespace GachaMoon.Common.DatabaseExtensions;

public static class UsersExtensions
{
    public static IQueryable<InternalUser> FilterUserByEmail(this IQueryable<InternalUser> query, FormattedEmail email)
    {
        return query
            .Where(x => x.Email == email.Value);
    }

    public static IQueryable<InternalUser> WhereUserIsAdmin(this IQueryable<InternalUser> query,
        long userId)
    {
        return query
            .IsNotDeleted()
            .Where(x => x.Id == userId && false); //todo admin accounts
    }

    public static void ThrowIfUserNotAdmin(this IQueryable<InternalUser> query,
        long userId)
    {
        var exists = query.WhereUserIsAdmin(userId)
            .Any();

        if (!exists)
        {
            throw new ArgumentException("User does not have Admin privileges");
        }
    }
}
