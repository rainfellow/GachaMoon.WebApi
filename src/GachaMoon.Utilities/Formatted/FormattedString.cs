namespace GachaMoon.Utilities.Formatted;

public abstract class FormattedString
{
    protected string FormattedValue { get; private set; } = default!;
    public string Value => FormattedValue;

    protected FormattedString(string data)
    {
        SetFormattedValue(data);
    }

    private void SetFormattedValue(string data)
    {
        FormattedValue = Format(data);

        if (!Validate())
        {
            throw new InputFormatException($"Value {data} couldn't be formatted. Expected format {ExpectedFormat}");
        }
    }

    protected abstract string Format(string data);
    protected abstract bool Validate();
    protected abstract string ExpectedFormat { get; }
}
