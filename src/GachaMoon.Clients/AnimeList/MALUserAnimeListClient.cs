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
    protected override ExternalClientType ClientType => ExternalClientType.UserAnimeList;

    private const string ApiUrl = "v2";
    private static readonly JsonSerializerOptions JsonSerializerOptions = new(JsonSerializerDefaults.Web);

    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<ICollection<UserAnimeData>> GetUserAnimeList(string userId, CancellationToken cancellationToken = default)
    {
        var keyValuePairs = new QueryBuilder()
        {
            { "limit", "1000" }
        };
        var uri = new Uri(
            $"{ApiUrl}/users/{userId}/animelist{keyValuePairs.ToQueryString()}", UriKind.Relative);
        var response = await HttpClient.GetAsync(uri, cancellationToken);
        var responseData = await response.Content.ReadFromJsonAsync<MALUserAnimeResponse>(JsonSerializerOptions);
        return responseData != null ? responseData.Data.Select(x => new UserAnimeData { Title = x.Node.Title, Id = x.Node.Id }).ToList() : [];
    }

    public async Task<UserAnimeData> GetAnime(string query, CancellationToken cancellationToken = default)
    {
        var response = await SendAnimeRequest(query, cancellationToken);
        var responseData = await response.Content.ReadFromJsonAsync<MALUserAnimeResponse>(JsonSerializerOptions);
        if (responseData == null || responseData.Data == null || responseData.Data.Count == 0)
        {
            response = await SendAnimeRequest($"{query.Replace(' ', '_')}", cancellationToken);
            responseData = await response.Content.ReadFromJsonAsync<MALUserAnimeResponse>(JsonSerializerOptions);
        }
        return responseData != null ? responseData.Data.Select(x => new UserAnimeData { Title = x.Node.Title, Id = x.Node.Id }).First() : throw new NotImplementedException("");
    }

    private async Task<HttpResponseMessage> SendAnimeRequest(string query, CancellationToken cancellationToken = default)
    {
        var keyValuePairs = new QueryBuilder()
        {
            { "q", query },
            { "limit", "1" }
        };
        var uri = new Uri(
            $"{ApiUrl}/anime{keyValuePairs.ToQueryString()}", UriKind.Relative);
        var response = await HttpClient.GetAsync(uri, cancellationToken);
        return response;
    }

    public async Task<UserAnimeData> GetAnimeDetails(int id, CancellationToken cancellationToken = default)
    {
        var keyValuePairs = new QueryBuilder()
        {
            { "fields", "id,title" }
        };
        var uri = new Uri(
            $"{ApiUrl}/anime/{id}{keyValuePairs.ToQueryString()}", UriKind.Relative);
        var response = await HttpClient.GetAsync(uri, cancellationToken);
        var responseData = await response.Content.ReadFromJsonAsync<MALAnimeNode>(JsonSerializerOptions);
        return responseData != null ? new UserAnimeData { Title = responseData.Title, Id = responseData.Id } : throw new NotImplementedException("");
    }
}
