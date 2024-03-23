using GachaMoon.Domain.Base;

namespace GachaMoon.Common.Query;

public static class QueryExtensions
{
    public static IQueryable<TEntity> IsNotDeleted<TEntity>(this IQueryable<TEntity> query)
        where TEntity : class, ISoftDeleteEntity
    {
        return query.Where(QueryExpressions.IsNotDeleted<TEntity>());
    }
}
