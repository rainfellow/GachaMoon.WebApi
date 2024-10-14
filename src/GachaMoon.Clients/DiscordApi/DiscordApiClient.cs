using System.Net.Http.Json;
using System.Text.Json;
using GachaMoon.Clients.Base;
using GachaMoon.Clients.DiscordApi.Data;
using GachaMoon.Services.Abstractions.Clients;
using GachaMoon.Services.Abstractions.Clients.Data;
using Microsoft.AspNetCore.Http.Extensions;
namespace GachaMoon.Clients.DiscordApi;

public class DiscordApiClient(
    IHttpClientFactory httpClientFactory) : HttpClientExternalBase(httpClientFactory), IDiscordApiClient
{
    protected override ExternalClientType ClientType => ExternalClientType.DiscordApi;

    //private const string ApiUrl = "api";
    private const string ApiUrl = "v1/discord";

    public async Task<DiscordUserData> CheckUserCredentials(string code, CancellationToken cancellationToken = default)
    {
        var keyValuePairs = new QueryBuilder()
        {
            { "code", code}

        };
        var queryString = keyValuePairs.ToQueryString();
        var uri = new Uri(
            $"{ApiUrl}{queryString}", UriKind.Relative);
        var response = await HttpClient.GetAsync(uri, cancellationToken);
        var responseData = await response.Content.ReadFromJsonAsync<DiscordUserData>(new JsonSerializerOptions(JsonSerializerDefaults.Web));
        if (responseData != null)
        {
            return responseData;
        }
        throw new NotImplementedException();
    }

    // public async Task<DiscordUserData> CheckUserCredentials(string code, CancellationToken cancellationToken = default)
    // {
    //     var token = await ExchangeCodeForToken(code, cancellationToken);
    //     var uri = new Uri(
    //         $"{ApiUrl}/users/@me", UriKind.Relative);
    //     var request = new HttpRequestMessage(HttpMethod.Get, uri);
    //     request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    //     var response = await HttpClient.SendAsync(request, cancellationToken);
    //     var responseData = await response.Content.ReadFromJsonAsync<DiscordUserData>(new JsonSerializerOptions(JsonSerializerDefaults.Web));
    //     if (responseData != null)
    //     {
    //         return responseData;
    //     }
    //     throw new NotImplementedException();
    // }


    private async Task<string> ExchangeCodeForToken(string code, CancellationToken cancellationToken = default)
    {
        var keyValuePairs = new QueryBuilder()
        {
            { "client_id", "1222238485134442556" },
            { "client_secret", "L_hqeJtY8iYUFm-2J1lDjubvXBQXcglW" },
            { "grant_type", "authorization_code" },
            { "redirect_uri", "https://gachamoon.xyz/auth/discord" },
            { "code", code}

        };
        var queryString = keyValuePairs.ToQueryString();
        var uri = new Uri(
            $"{ApiUrl}/oauth2/token", UriKind.Relative);
        var content = new StringContent(queryString.ToUriComponent().Remove(0, 1), System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");
        var response = await HttpClient.PostAsync(uri, content, cancellationToken);
        var responseData = await response.Content.ReadFromJsonAsync<DiscordTokenData>(new JsonSerializerOptions(JsonSerializerDefaults.Web));
        if (responseData != null)
        {
            return responseData.AccessToken;
        }
        throw new NotImplementedException();
    }
}
