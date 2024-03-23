namespace GachaMoon.Common.Exceptions;

#pragma warning disable CA1032, CA1710
public abstract class SafeMessageExceptionBase : Exception
{
    public abstract string SafeMessage { get; }
}
