namespace GachaMoon.Clients.Extensions;

public static class HttpClientExtensions
{
    public static async Task<byte[]> RetrieveFileAsBytes(this HttpClient httpClient, Uri url)
    {
        using var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        using var responseStream = await response.Content.ReadAsStreamAsync();
        using var memoryStream = new MemoryStream();
        {
            await responseStream.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
