namespace GachaMoon.Common.Exceptions.Users;

public class UserAlreadyExistsException : Exception
{
    public string CollisionField { get; private set; } = "Unknown";

    public UserAlreadyExistsException(string collisionField) : base()
    {
        CollisionField = collisionField;
    }

    public UserAlreadyExistsException()
    {
    }

    public UserAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
