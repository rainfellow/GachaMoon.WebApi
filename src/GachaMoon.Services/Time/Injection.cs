using GachaMoon.Services.Abstractions.Time;
using Microsoft.Extensions.DependencyInjection;

namespace GachaMoon.Services.Time;

public static class Injection
{
    public static IServiceCollection AddSystemClockProvider(this IServiceCollection services)
    {
        return services.AddSingleton<IClockProvider, SystemClockProvider>();
    }
}
