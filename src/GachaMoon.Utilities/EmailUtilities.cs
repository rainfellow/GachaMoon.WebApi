using System.ComponentModel.DataAnnotations;
using GachaMoon.Utilities.Formatted;

namespace GachaMoon.Utilities;

public static class EmailUtilities
{
    public static bool ValidateEmail(FormattedEmail formattedEmail)
    {
        var attribute = new EmailAddressAttribute();
        return attribute.IsValid(formattedEmail.Value);
    }

    public static FormattedEmail FormatEmail(string email)
    {
        return new FormattedEmail(email);
    }
}
