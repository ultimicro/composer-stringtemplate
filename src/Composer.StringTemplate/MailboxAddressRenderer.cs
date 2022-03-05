namespace Composer.StringTemplate;

using System;
using System.Globalization;
using Antlr4.StringTemplate;
using MimeKit;

internal sealed class MailboxAddressRenderer : IAttributeRenderer
{
    public string ToString(object obj, string formatString, CultureInfo culture)
    {
        var v = (MailboxAddress)obj;

        return formatString switch
        {
            "name" => v.Name,
            "address" => v.Address,
            "full" => v.ToString(),
            _ => throw new ArgumentException($"Unknown format '{formatString}'.", nameof(formatString)),
        };
    }
}
