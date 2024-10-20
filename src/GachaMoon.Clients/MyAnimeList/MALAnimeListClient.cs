using System.Net.Http.Json;
using System.Text.Json;
using GachaMoon.Clients.MyAnimeList.Data;
using GachaMoon.Clients.Base;
using GachaMoon.Services.Abstractions.Clients;
using GachaMoon.Services.Abstractions.Clients.Data;
using Microsoft.AspNetCore.Http.Extensions;

namespace GachaMoon.Clients.MyAnimeList;

public class MALAnimeListClient(IHttpClientFactory httpClientFactory) : HttpClientExternalBase(httpClientFactory), IMyAnimeListApiClient
{
    protected override ExternalClientType ClientType => ExternalClientType.UserAnimeList;

    private const string ApiUrl = "v2";
    private static readonly JsonSerializerOptions JsonSerializerOptions = new(JsonSerializerDefaults.Web);


    public async Task<ICollection<UserAnimeData>> GetUserAnimeList(string userId, ICollection<string> animeStatuses, CancellationToken cancellationToken = default)
    {
        var result = new List<UserAnimeData>();
        foreach (var animeStatus in animeStatuses)
        {
            var keyValuePairs = new QueryBuilder()
            {
                { "limit", "1000" },
                { "status", GetStatusMALName(animeStatus) }
            };
            var uri = new Uri(
                $"{ApiUrl}/users/{userId}/animelist{keyValuePairs.ToQueryString()}", UriKind.Relative);
            var response = await HttpClient.GetAsync(uri, cancellationToken);
            var responseData = await response.Content.ReadFromJsonAsync<MALUserAnimeResponse>(JsonSerializerOptions);
            result.AddRange(responseData != null ? responseData.Data.Select(x => new UserAnimeData { Title = x.Node.Title, Id = x.Node.Id }) : []);
        }
        return result;
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

    public async Task<AnimeDetailedData> GetAnimeDetails(int id, CancellationToken cancellationToken = default)
    {
        var keyValuePairs = new QueryBuilder()
        {
            { "fields", "id,title,alternative_titles,start_date,mean,media_type,num_episodes,rating" }
        };
        var uri = new Uri(
            $"{ApiUrl}/anime/{id}{keyValuePairs.ToQueryString()}", UriKind.Relative);
        var response = await HttpClient.GetAsync(uri, cancellationToken);
        var responseData = await response.Content.ReadFromJsonAsync<MALAnimeNode>(JsonSerializerOptions);
        return responseData != null ? new AnimeDetailedData
        {
            Title = responseData.Title,
            MalId = responseData.Id,
            AnimeType = responseData.MediaType,
            StartDate = responseData.StartDate,
            MeanScore = responseData.Mean,
            EpisodeCount = responseData.NumEpisodes,
            AgeRating = responseData.Rating,
        }
        : throw new NotImplementedException("");
    }

    private static string GetStatusMALName(string status)
    {
        return status switch
        {
            "watching" => "watching",
            "completed" => "completed",
            "paused" => "on_hold",
            "dropped" => "dropped",
            "planned" => "plan_to_watch",
            _ => "Unknown",
        };
    }
}
