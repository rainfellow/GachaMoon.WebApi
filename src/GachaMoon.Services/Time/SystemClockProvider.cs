using GachaMoon.Services.Abstractions.Time;
using NodaTime;

namespace GachaMoon.Services.Time;

public class SystemClockProvider : IClockProvider
{
    public Instant Now => SystemClock.Instance.GetCurrentInstant();
}
