using System.Net.Http.Json;
using System.Text.Json;
using GachaMoon.Clients.Base;
using GachaMoon.Services.Abstractions.Clients;
using GachaMoon.Services.Abstractions.Clients.Data;
using Microsoft.AspNetCore.Http.Extensions;

namespace GachaMoon.Clients.AnimeQuiz;

public class AnimeQuizApiClient(
    IHttpClientFactory httpClientFactory) : HttpClientExternalBase(httpClientFactory), IAnimeQuizClient
{
    protected override ExternalClientType ClientType => ExternalClientType.AnimeQuizApi;

    private const string ApiUrl = "v1/animes";

    public async Task<AnimeQuizResult> GetQuizResult(string answer, CancellationToken cancellationToken = default)
    {
        var keyValuePairs = new QueryBuilder()
        {
            { "answer", answer },
        };
        var queryString = keyValuePairs.ToQueryString();
        var uri = new Uri(
            $"{ApiUrl}{queryString}", UriKind.Relative);
        var response = await HttpClient.GetAsync(uri, cancellationToken);
        var responseData = await response.Content.ReadFromJsonAsync<AnimeQuizResult>(new JsonSerializerOptions(JsonSerializerDefaults.Web));
        return responseData ?? new AnimeQuizResult();
    }
}
