namespace GachaMoon.Clients.Base;

public abstract class HttpClientExternalBase
{
    protected abstract ExternalClientType ClientType { get; }
    protected HttpClient HttpClient { get; private set; }

    protected HttpClientExternalBase(IHttpClientFactory httpClientFactory)
    {
        HttpClient = httpClientFactory.CreateClient(ClientType.ToString());
    }
}
