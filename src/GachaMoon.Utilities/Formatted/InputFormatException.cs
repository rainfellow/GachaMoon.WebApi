namespace GachaMoon.Utilities.Formatted;

public class InputFormatException : Exception
{
    public InputFormatException(string? message) : base(message)
    {
    }

    public InputFormatException()
    {
    }

    public InputFormatException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
