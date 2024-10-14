namespace GachaMoon.Common.Exceptions.Users;

public class DiscordUserNotFoundException : Exception
{
    public DiscordUserNotFoundException(string id) : base($"User with discord ID {id} doesn't exist")
    {
    }

    public DiscordUserNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public DiscordUserNotFoundException()
    {
    }
}
