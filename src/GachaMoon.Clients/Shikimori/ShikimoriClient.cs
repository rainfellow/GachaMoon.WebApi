using GachaMoon.Services.Abstractions.Clients;
using GachaMoon.Services.Abstractions.Clients.Data;
using GraphQL.Client.Abstractions;
using GraphQL;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using GachaMoon.Clients.Shikimori.Data;

namespace GachaMoon.Clients.Shikimori;

public class ShikimoriGraphqlClient([FromKeyedServices("ShikimoriGraphql")] IGraphQLClient graphqlClient, ILogger<ShikimoriGraphqlClient> logger) : IShikimoriClient
{
    private const string ApiUrl = "v2";
    private readonly IGraphQLClient _graphqlClient = graphqlClient;
    private readonly ILogger<ShikimoriGraphqlClient> _logger = logger;

    public async Task<ICollection<UserAnimeData>> GetUserAnimeList(string userId, ICollection<string> animeStatuses, CancellationToken cancellationToken = default)
    {
        var userLists = await GetUserAnimes(userId);

        var userAnimes = userLists
            .Where(x => animeStatuses.Contains(StatusMALNameToInternal(x.Status)))
            .Select(x => new UserAnimeData
            {
                Id = x.Anime.Id,
                Title = x.Anime.Russian
            })
            .ToList();

        return userAnimes;
    }

    private static string StatusMALNameToInternal(string status)
    {
        return status switch
        {
            "watching" => "watching",
            "completed" => "completed",
            "on_hold" => "paused",
            "dropped" => "dropped",
            "plan_to_watch" => "planned",
            _ => "Unknown",
        };
    }

    private async Task<List<ShikimoriUserAnime>> GetUserAnimes(string userName)
    {
        var userQuery = new GraphQLRequest
        {
            Query = @"
                    query userQuery($userName: String)
                    {
                        users(page: 1, limit: 1, search: $userName) {
                            id
                            nickname
                        }
                    }",
            Variables = new { userName = userName }
        };
        var userResponse = await _graphqlClient.SendQueryAsync<ShikimoriUserResponse>(userQuery);
        var userShikimoriId = userResponse.Data.Users[0].Id;
        var pageNum = 1;
        var emptyResponse = false;
        var userAnimes = new List<ShikimoriUserAnime>();
        while (!emptyResponse)
        {
            var listQuery = new GraphQLRequest
            {
                Query = @"
                        query userListQuery($pageNum: PositiveInt, $userId: ID!)
                        {
                            userRates(page: $pageNum, limit: 100, userId: $userId, targetType: Anime) {
                                anime {
                                    id
                                    russian
                                }
                                status
                            }
                        }",
                Variables = new { userId = userShikimoriId, pageNum = pageNum }
            };
            var response = await _graphqlClient.SendQueryAsync<ShikimoriUserListResponse>(listQuery);
            if (response.Data.UserRates.Count == 0)
            {
                emptyResponse = true;
            }
            else
            {
                userAnimes.AddRange(response.Data.UserRates);
                pageNum++;
                await Task.Delay(400);
            }
        }
        return userAnimes;
    }

    public async Task<ShikimoriAnimeData> GetAnimeDetails(long malId, CancellationToken cancellationToken = default)
    {
        var animeQuery = new GraphQLRequest
        {
            Query = @"
                    query animeQuery($animeId: String)
                    {
                        animes(limit: 1, ids: $animeId) {
                            english
                            russian
                        }
                    }",
            Variables = new { animeId = malId.ToString() }
        };
        var response = await _graphqlClient.SendQueryAsync<ShikimorAnimeDetailsResponse>(animeQuery);
        return response.Data.Animes.Count > 0 ? response.Data.Animes[0] : new ShikimoriAnimeData();
    }
}
