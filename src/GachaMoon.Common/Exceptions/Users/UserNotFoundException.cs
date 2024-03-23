namespace GachaMoon.Common.Exceptions.Users;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string message) : base(message)
    {
    }

    public UserNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public UserNotFoundException()
    {
    }

    public UserNotFoundException(long userId) : base($"User with ID {userId} doesn't exist")
    {
    }
}
