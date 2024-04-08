using System.Net.Http.Json;
using System.Text.Json;
using GachaMoon.Clients.Base;
using GachaMoon.Services.Abstractions.Clients;
using GachaMoon.Services.Abstractions.Clients.Data;
using Microsoft.AspNetCore.Http.Extensions;

namespace GachaMoon.Clients.Anime;

public class ApiAnimeListClient(
    IHttpClientFactory httpClientFactory) : HttpClientExternalBase(httpClientFactory), IAnimeClient
{
    protected override ExternalClientType ClientType => ExternalClientType.AnimeList;

    private const string ApiUrl = "api";


    public Task<AnimeData> RandomAnime(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<AnimeData>> AnimeFromQuery(string query, CancellationToken cancellationToken = default)
    {
        var keyValuePairs = new QueryBuilder()
        {
            { "query", query },
            { "provider", "Anilist" },
            { "includeAdult", "true" },
            { "collectionConsent", "true" }
        };
        var uri = new Uri(
            $"{ApiUrl}?{keyValuePairs}");
        var response = await HttpClient.GetAsync(uri, cancellationToken);
        var responseData = await response.Content.ReadFromJsonAsync<ICollection<AnimeData>>(new JsonSerializerOptions(JsonSerializerDefaults.Web));
        return responseData ?? new List<AnimeData>();
    }
}
