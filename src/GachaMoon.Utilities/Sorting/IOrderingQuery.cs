using System.Linq.Expressions;

namespace GachaMoon.Utilities.Sorting;

public interface IOrderingQuery<TEntity> : ISortingContainer
{
    IReadOnlyDictionary<string, Expression<Func<TEntity, object?>>> GetOrderingPropertyMappings();
    IReadOnlyCollection<OrderByFunction<TEntity>> GetDefaultOrdering();
}
