namespace Composer.StringTemplate;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Antlr4.StringTemplate;
using MimeKit;

public sealed class Template : Composer.Template
{
    private readonly object id;
    private readonly TemplateGroup template;

    public Template(object id, CultureInfo culture, MailboxAddress sender, string template, IEnumerable<IPropertyRenderer> renderers)
        : base(culture, sender)
    {
        this.id = id;
        this.template = new TemplateGroupString("[string]", template, '$', '$');
        this.template.RegisterRenderer(typeof(MailboxAddress), new MailboxAddressRenderer());

        foreach (var renderer in renderers)
        {
            this.template.RegisterRenderer(renderer.TargetType, renderer);
        }
    }

    public override ValueTask<string> BuildSubjectAsync(IEnumerable<InternetAddress> recipients, object? data, CancellationToken cancellationToken = default)
    {
        var template = this.template.GetInstanceOf("subject");

        if (template == null)
        {
            throw new InvalidOperationException($"Template {this.id} does not contains 'subject' definition.");
        }

        template.Add("recipients", recipients);
        template.Add("data", data);

        return new ValueTask<string>(template.Render(this.Culture));
    }

    protected override ValueTask<string?> BuildPlainMessageAsync(
        IEnumerable<InternetAddress> recipients,
        object? data,
        CancellationToken cancellationToken = default)
    {
        var template = this.template.GetInstanceOf("plain");

        if (template == null)
        {
            return new ValueTask<string?>((string?)null);
        }

        template.Add("recipients", recipients);
        template.Add("data", data);

        return new ValueTask<string?>(template.Render(this.Culture));
    }
}
