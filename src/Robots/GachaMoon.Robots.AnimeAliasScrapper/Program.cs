using Ensersoft.Robots;
using GachaMoon.Database;
using GachaMoon.Services.Time;
using GachaMoon.Clients.Anime;
using GachaMoon.Clients.Shikimori;

namespace GachaMoon.Robots.AnimeAliasScrapper;

public class Program : CronJobBase
{
    private const string OperationName = "run";
    private const string RoleName = "anime-alias-scrapper";

    public static Task Main()
    {
        return Job(OperationName, RoleName, Configure);
    }

    private static void Configure(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDatabase(configuration);
        services.AddSystemClockProvider();

        services.AddAnimeClients();
        services.AddShikimoriClients();

        services.AddSingleton<IRobot, AnimeAliasScrapperRobot>();
    }
}
