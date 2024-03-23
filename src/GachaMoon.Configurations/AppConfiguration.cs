using Microsoft.Extensions.Configuration;

namespace GachaMoon.Configurations;

public static class AppConfiguration
{
    public static IConfigurationBuilder ApplyAppConfiguration(this IConfigurationBuilder builder, string[]? args = null)
    {
        builder
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
            .AddJsonFile("secrets/shared.json", optional: true, reloadOnChange: true)
            .AddJsonFile("secrets/app.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        if (args != null)
        {
            builder.AddCommandLine(args);
        }

        return builder;
    }
}
