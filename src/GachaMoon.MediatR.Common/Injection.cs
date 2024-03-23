using GachaMoon.MediatR.Common.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace GachaMoon.MediatR.Common;

public static class Injection
{
    public static IServiceCollection AddCommonBehaviors(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AdminAccessBehavior<,>));

        return services;
    }
}
