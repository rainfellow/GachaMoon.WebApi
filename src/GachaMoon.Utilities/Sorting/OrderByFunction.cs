using System.Linq.Expressions;

namespace GachaMoon.Utilities.Sorting;

public class OrderByFunction<T>(Expression<Func<T, object?>> selector, bool descending)
{
    public bool Descending { get; private set; } = descending;
    public Expression<Func<T, object?>> Selector { get; private set; } = selector;
}
