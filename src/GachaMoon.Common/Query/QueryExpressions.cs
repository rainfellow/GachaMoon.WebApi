using System.Linq.Expressions;
using GachaMoon.Domain.Base;

namespace GachaMoon.Common.Query;

public static class QueryExpressions
{
    public static Expression<Func<TEntity, bool>> IsNotDeleted<TEntity>()
        where TEntity : class, ISoftDeleteEntity
    {
        return e => e.DeletedAt == null;
    }
}
