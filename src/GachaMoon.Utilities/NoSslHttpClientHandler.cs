namespace GachaMoon.Utilities;

public class NoSslHttpClientHandler : DefaultHttpClientHandler
{
    public NoSslHttpClientHandler()
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
    }
}
