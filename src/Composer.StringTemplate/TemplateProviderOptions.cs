namespace Composer.StringTemplate;

/// <summary>
/// Options for <see cref="TemplateProvider"/>.
/// </summary>
public sealed class TemplateProviderOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether to disable fallback to the parent culture.
    /// </summary>
    /// <remarks>
    /// If this flag is <c>false</c> and the requested template with culture "de-Latn-DE" does not exists it will try again with "de-Latn" then
    /// "de" then "" (invariant culture). Set this flag to <c>true</c> to disable this behavior.
    ///
    /// The default value is <c>false</c>.
    /// </remarks>
    public bool DisabledParentCultureFallback { get; set; }
}
