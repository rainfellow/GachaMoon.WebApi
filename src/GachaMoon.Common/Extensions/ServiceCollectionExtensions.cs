using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GachaMoon.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAndValidateOptions<T>(this IServiceCollection services, IConfiguration configuration, string section)
        where T : class
    {
        services
            .AddOptions<T>()
            .Bind(configuration.GetSection(section))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton(sp =>
        {
            try
            {
                return sp.GetRequiredService<IOptions<T>>().Value;
            }
            catch (Exception e)
            {
                var logger = sp.GetRequiredService<ILogger<T>>();
                logger.LogCritical(e, "Failed to validate options of type {OptionsType}", typeof(T).FullName);
                throw;
            }
        });

        return services;
    }
}
