using NodaTime;

namespace GachaMoon.Common.Extensions;

public static class SystemClockExtensions
{
    public static Instant Now(this SystemClock clock)
    {
        return clock.GetCurrentInstant();
    }

    public static string ToString(this LocalDate localDate, string format)
    {
        return localDate.ToString(format, null);
    }

    public static string ToString(this Instant instant, string format)
    {
        return instant.ToString(format, null);
    }
}
