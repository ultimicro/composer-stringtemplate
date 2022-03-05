namespace Composer.StringTemplate;

using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// A service to provides <see cref="TemplateData"/>.
/// </summary>
public interface ITemplateRepository
{
    /// <summary>
    /// Find a template data by template identifier and culture.
    /// </summary>
    /// <param name="id">
    /// The identifier of the template.
    /// </param>
    /// <param name="culture">
    /// The culture of the template.
    /// </param>
    /// <param name="cancellationToken">
    /// The token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A template data for the speicfied identifier and culture or <c>null</c> if no such template.
    /// </returns>
    ValueTask<TemplateData?> GetByIdAsync(object id, CultureInfo culture, CancellationToken cancellationToken = default);
}
