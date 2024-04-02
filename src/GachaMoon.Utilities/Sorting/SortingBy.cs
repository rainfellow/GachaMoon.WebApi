namespace GachaMoon.Utilities.Sorting;

public class SortingBy(string propertyName, bool isDescending)
{
    public string PropertyName { get; } = propertyName;
    public bool IsDescending { get; } = isDescending;
}
