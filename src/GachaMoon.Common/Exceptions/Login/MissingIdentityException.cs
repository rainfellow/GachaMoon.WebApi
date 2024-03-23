namespace GachaMoon.Common.Exceptions.Login;

public class MissingIdentityException : Exception
{
    public MissingIdentityException(string message) : base(message)
    {
    }

    public MissingIdentityException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public MissingIdentityException() : base("User Identity is missing")
    {
    }
}
