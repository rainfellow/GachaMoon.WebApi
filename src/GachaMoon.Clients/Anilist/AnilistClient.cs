using GachaMoon.Services.Abstractions.Clients;
using GachaMoon.Services.Abstractions.Clients.Data;
using GraphQL.Client.Abstractions;
using GachaMoon.Clients.Anilist.Data;
using GraphQL;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace GachaMoon.Clients.Anilist;

public class AnilistGraphqlClient([FromKeyedServices("AnilistGraphql")] IGraphQLClient graphqlClient, ILogger<AnilistGraphqlClient> logger) : IAnilistClient
{
    private const string ApiUrl = "v2";
    private readonly IGraphQLClient _graphqlClient = graphqlClient;
    private readonly ILogger<AnilistGraphqlClient> _logger = logger;

    public async Task<ICollection<UserAnimeData>> GetUserAnimeList(string userId, ICollection<string> animeStatuses, CancellationToken cancellationToken = default)
    {
        var userLists = await GetUserAnimes(userId, animeStatuses);

        var userAnimes = userLists
            .SelectMany(x => x.Entries)
            .Select(x => new UserAnimeData
            {
                Id = x.Media.Id,
                Title = x.Media.Title.Romaji
            })
            .ToList();

        return userAnimes;
    }

    private static string GetStatusAnilistName(string status)
    {
        return status switch
        {
            "watching" => "CURRENT",
            "completed" => "COMPLETED,REPEATING",
            "paused" => "PAUSED",
            "dropped" => "DROPPED",
            "planned" => "PLANNING",
            _ => "COMPLETED",
        };
    }

    private async Task<List<AnilistList>> GetUserAnimes(string userName, ICollection<string> animeStatuses)
    {
        var animeStatusesList = animeStatuses.Select(GetStatusAnilistName).Aggregate("", (current, s) => current + s + ",").Split(",", StringSplitOptions.RemoveEmptyEntries);
        var query = new GraphQLRequest
        {
            Query = @"
                    query userListQuery($userName: String!, $statuses: [MediaListStatus]!) {
                        MediaListCollection(type: ANIME, userName: $userName, status_in: $statuses) {
                            lists {
                                name
                                entries {
                                    media {
                                        id
                                        title {
                                            romaji
                                        }
                                    }
                                }
                            }
                        }
                    }",
            Variables = new { userName = userName, statuses = animeStatusesList }
        };
        var response = await _graphqlClient.SendQueryAsync<AnilistUserListResponse>(query);
        return response.Data.MediaListCollection.Lists;
    }
}
