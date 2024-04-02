using System.ComponentModel.DataAnnotations;

namespace GachaMoon.Utilities.Formatted;

public class FormattedEmail(string data) : FormattedString(data)
{
    protected override string ExpectedFormat => "address@example.com";

    protected override string Format(string data)
    {
#pragma warning disable CA1308 
        return data.Trim().ToLowerInvariant();
#pragma warning restore CA1308 
    }

    protected override bool Validate()
    {
        var attribute = new EmailAddressAttribute();
        return attribute.IsValid(FormattedValue);
    }
}
