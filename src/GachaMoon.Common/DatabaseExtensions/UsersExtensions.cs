using GachaMoon.Common.Query;
using GachaMoon.Domain.Accounts;
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

    public static IQueryable<Account> WhereUserIsAdmin(this IQueryable<Account> query,
        long accountId)
    {
        return query
            .IsNotDeleted()
            .Where(x => x.Id == accountId && x.AccountType == AccountType.Admin);
    }

    public static void ThrowIfUserNotAdmin(this IQueryable<Account> query,
        long accountId)
    {
        var exists = query.WhereUserIsAdmin(accountId)
            .Any();

        if (!exists)
        {
            throw new ArgumentException("User does not have Admin privileges");
        }
    }
}
