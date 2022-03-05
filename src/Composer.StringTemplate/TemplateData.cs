namespace Composer.StringTemplate;

using MimeKit;

/// <summary>
/// Represent a template data provided by <see cref="ITemplateRepository"/>.
/// </summary>
public sealed class TemplateData
{
    public TemplateData(MailboxAddress sender, string template)
    {
        this.Sender = sender;
        this.Template = template;
    }

    public MailboxAddress Sender { get; }

    public string Template { get; }
}
