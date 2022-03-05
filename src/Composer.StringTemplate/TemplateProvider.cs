namespace Composer.StringTemplate;

using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

public sealed class TemplateProvider : ITemplateProvider
{
    private readonly TemplateProviderOptions options;
    private readonly ITemplateRepository repository;
    private readonly IEnumerable<IPropertyRenderer> renderers;

    public TemplateProvider(IOptions<TemplateProviderOptions> options, ITemplateRepository repository, IEnumerable<IPropertyRenderer> renderers)
    {
        this.options = options.Value;
        this.repository = repository;
        this.renderers = renderers;
    }

    public async ValueTask<Composer.Template?> GetByIdAsync(object id, CultureInfo culture, CancellationToken cancellationToken = default)
    {
        var data = await this.repository.GetByIdAsync(id, culture, cancellationToken);

        if (data == null && !this.options.DisabledParentCultureFallback && !culture.Equals(CultureInfo.InvariantCulture))
        {
            do
            {
                culture = culture.Parent;
                data = await this.repository.GetByIdAsync(id, culture, cancellationToken);
            }
            while (data == null && !culture.Equals(CultureInfo.InvariantCulture));
        }

        if (data == null)
        {
            return null;
        }

        return new Template(id, culture, data.Sender, data.Template, this.renderers);
    }
}
