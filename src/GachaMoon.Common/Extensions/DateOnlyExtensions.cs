namespace GachaMoon.Common.Extensions;

public static class DateOnlyExtensions
{
    public static DateOnly GetLastDayOfMonth(this DateOnly dateOnly)
    {
        return new DateOnly(dateOnly.Year, dateOnly.Month, 1).AddMonths(1).AddDays(-1);
    }

    public static bool IsSameYearMonth(this DateOnly dateOnly, DateOnly dateOnly2)
    {
        return dateOnly.Year == dateOnly2.Year && dateOnly.Month == dateOnly2.Month;
    }
}
