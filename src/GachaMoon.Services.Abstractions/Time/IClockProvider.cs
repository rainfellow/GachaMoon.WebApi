using NodaTime;

namespace GachaMoon.Services.Abstractions.Time;

public interface IClockProvider
{
    Instant Now { get; }
}
