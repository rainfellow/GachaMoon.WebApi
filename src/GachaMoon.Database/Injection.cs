using GachaMoon.Services.Abstractions.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GachaMoon.Database;

public static class Injection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContextFactory<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), x =>
                {
                    _ = x.EnableRetryOnFailure();
                    _ = x.CommandTimeout(30);
                    _ = x.MigrationsAssembly("GachaMoon.Database.Migrations");
                    _ = x.UseNodaTime();
                }
            )
        );
    }

    public static IServiceCollection AddDatabaseInitialization(
        this IServiceCollection services)
    {
        services.TryAddTransient<IDatabaseInitializer, DatabaseInitializer>();
        return services;
    }
}
