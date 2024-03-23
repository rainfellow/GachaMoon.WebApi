namespace GachaMoon.Common.Exceptions.Login;

public class LoginFailedException : Exception
{
    public LoginFailedException(string? message) : base(message)
    {
    }

    public LoginFailedException()
    {
    }

    public LoginFailedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
