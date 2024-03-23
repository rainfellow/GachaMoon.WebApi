using System.Net;

namespace GachaMoon.Utilities.Http;

public class DefaultHttpClientHandler : HttpClientHandler
{
    public DefaultHttpClientHandler()
    {
        AutomaticDecompression =
            DecompressionMethods.Brotli |
            DecompressionMethods.Deflate |
            DecompressionMethods.GZip;
    }
}
