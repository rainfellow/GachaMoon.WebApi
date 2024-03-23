namespace GachaMoon.Common.Exceptions.Users;

public class WrongPasswordException : Exception
{
    public WrongPasswordException(string? message) : base(message)
    {
    }

    public WrongPasswordException()
    {
    }

    public WrongPasswordException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
