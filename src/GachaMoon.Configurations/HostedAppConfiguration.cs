using Microsoft.Extensions.Configuration;

namespace GachaMoon.Configurations;

public static class HostedAppConfiguration
{
    public static void ApplyHostedAppConfiguration(this IConfigurationBuilder builder, string[]? args = null)
    {
        while (builder.Sources.Count > 1)
        {
            builder.Sources.RemoveAt(1);
        }

        builder.ApplyAppConfiguration(args);
    }
}
