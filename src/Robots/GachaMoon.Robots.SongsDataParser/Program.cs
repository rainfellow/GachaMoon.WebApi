using Ensersoft.Robots;
using GachaMoon.Clients.MyAnimeList;
using GachaMoon.Database;
using GachaMoon.Services.Abstractions.Anime;
using GachaMoon.Services.Time;
using GachaMoon.Services.Anime;
using GachaMoon.Clients.Anime;
using GachaMoon.Clients.AnisongDB;

namespace GachaMoon.Robots.SongsDataParser;

public class Program : CronJobBase
{
    private const string OperationName = "run";
    private const string RoleName = "songs-data-parser";

    public static Task Main()
    {
        return Job(OperationName, RoleName, Configure);
    }

    private static void Configure(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDatabase(configuration);
        services.AddSystemClockProvider();

        services.AddSingleton<IUserSavedAnimeListService, UserSavedAnimeListService>();
        services.AddUserAnimeListClients();
        services.AddUserAnisongDBClients();
        services.AddAnimeClients();

        services.AddSingleton<IRobot, SongsDataParserRobot>();
    }
}
