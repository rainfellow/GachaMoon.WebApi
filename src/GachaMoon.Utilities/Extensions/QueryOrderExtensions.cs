using System.Linq.Expressions;
using GachaMoon.Utilities.Sorting;

namespace GachaMoon.Utilities.Extensions;

public static class QueryOrderExtensions
{
    /// <summary>
    /// Orders IQueryable by multiple properties,
    /// using selectors from <paramref name="orderByFunctions"/>.
    /// Ordering is applied in correspondence with the order of the selectors.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <param name="source"></param>
    /// <param name="orderByFunctions">
    /// Specifies selectors and direction to order <paramref name="source"/>.
    /// </param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="orderByFunctions"/> is empty.</exception>
    /// <returns>IOrderedQueryable, OrderedBy first selector and ThenBy others</returns>
    public static IOrderedQueryable<TSource> ApplyOrderByFunctions<TSource>
        (this IQueryable<TSource> source,
         ICollection<OrderByFunction<TSource>> orderByFunctions)
    {
        var first = orderByFunctions.FirstOrDefault()
            ?? throw new ArgumentException("OrderByFunctions collection can't be empty");
        var orderedSource = source.OrderByWithDirection(first.Selector, first.Descending);

        foreach (var func in orderByFunctions.Skip(1))
        {
            orderedSource = orderedSource.ThenByWithDirection(func.Selector, func.Descending);
        }

        return orderedSource;
    }

    private static IOrderedQueryable<TSource> OrderByWithDirection<TSource, TKey>
        (this IQueryable<TSource> source,
         Expression<Func<TSource, TKey>> keySelector,
         bool descending)
    {
        return descending ? source.OrderByDescending(keySelector)
                          : source.OrderBy(keySelector);
    }

    private static IOrderedQueryable<TSource> ThenByWithDirection<TSource, TKey>
        (this IOrderedQueryable<TSource> source,
         Expression<Func<TSource, TKey>> keySelector,
         bool descending)
    {
        return descending ? source.ThenByDescending(keySelector)
                          : source.ThenBy(keySelector);
    }
}
