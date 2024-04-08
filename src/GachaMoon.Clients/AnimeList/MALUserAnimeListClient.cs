using System.Net.Http.Json;
using System.Text.Json;
using GachaMoon.Clients.AnimeList.Data;
using GachaMoon.Clients.Base;
using GachaMoon.Database;
using GachaMoon.Services.Abstractions.Clients;
using GachaMoon.Services.Abstractions.Clients.Data;
using Microsoft.AspNetCore.Http.Extensions;

namespace GachaMoon.Clients.AnimeList;

public class MALUserAnimeListClient(ApplicationDbContext dbContext,
    IHttpClientFactory httpClientFactory) : HttpClientExternalBase(httpClientFactory), IUserAnimeListClient
{
    protected override ExternalClientType ClientType => ExternalClientType.AnimeList;

    private const string ApiUrl = "v2";

    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<ICollection<UserAnimeData>> GetUserAnimeList(string userId, CancellationToken cancellationToken = default)
    {
        var keyValuePairs = new QueryBuilder()
        {
            { "user_name", userId },
        };
        var uri = new Uri(
            $"{ApiUrl}?{keyValuePairs}");
        var response = await HttpClient.GetAsync(uri, cancellationToken);
        var responseData = await response.Content.ReadFromJsonAsync<MALUserAnimeResponse>(new JsonSerializerOptions(JsonSerializerDefaults.Web));
        return responseData != null ? responseData.Data.Select(x => new UserAnimeData { Title = x.Node.Title }).ToList() : [];
    }
}
