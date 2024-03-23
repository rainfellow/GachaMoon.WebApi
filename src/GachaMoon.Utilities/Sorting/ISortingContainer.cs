namespace GachaMoon.Utilities.Sorting;

public interface ISortingContainer
{
    IReadOnlyCollection<SortingBy>? Sortings { get; }
    IReadOnlySet<string> GetPropertyKeys();
}
