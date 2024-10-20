using System.Net.Http.Json;
using System.Text.Json;
using GachaMoon.Clients.Base;
using GachaMoon.Services.Abstractions.Clients;
using GachaMoon.Services.Abstractions.Clients.Data;
using GachaMoon.Clients.AnisongDB.Data;
using Microsoft.AspNetCore.Http;

namespace GachaMoon.Clients.AnisongDB;

public class AnisongDBClient(IHttpClientFactory httpClientFactory) : HttpClientExternalBase(httpClientFactory), IAnisongDBClient
{
    protected override ExternalClientType ClientType => ExternalClientType.AnisongDB;

    private const string ApiUrl = "api";
    private static readonly JsonSerializerOptions JsonSerializerOptions = new(JsonSerializerDefaults.Web);


    public async Task<ICollection<AnisongSongData>> GetAnimeSongsData(string amqAnimeName, CancellationToken cancellationToken = default)
    {
        var uri = new Uri(
            $"{ApiUrl}/search_request", UriKind.Relative);
        var request = new AnisongDBRequest(amqAnimeName);
        var response = await HttpClient.PostAsJsonAsync(uri, request, cancellationToken);
        if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
        {
            return new List<AnisongSongData>();
        }
        var responseData = await response.Content.ReadFromJsonAsync<ICollection<AnisongSongData>>(JsonSerializerOptions);
        return responseData ?? new List<AnisongSongData>();

    }
}
