namespace Microsoft.Extensions.DependencyInjection;

using System;
using Composer.StringTemplate;

public static class ComposerBuilderExtensions
{
    /// <summary>
    /// Register StringTemplate 4 as a template provider.
    /// </summary>
    /// <param name="builder">
    /// The builder to register to.
    /// </param>
    /// <param name="options">
    /// The delegate to configure options.
    /// </param>
    /// <returns>
    /// <paramref name="builder"/> to chain the call.
    /// </returns>
    /// <remarks>
    /// You need to provide an implementation of <see cref="ITemplateRepository"/>.
    /// </remarks>
    public static ComposerBuilder AddStringTemplate(this ComposerBuilder builder, Action<TemplateProviderOptions>? options = null)
    {
        if (options != null)
        {
            builder.Services.Configure(options);
        }
        else
        {
            builder.Services.Configure<TemplateProviderOptions>(_ => { });
        }

        builder.AddTemplateProvider<TemplateProvider>();

        return builder;
    }

    /// <summary>
    /// Register an implementation of <see cref="ITemplateRepository"/> to use.
    /// </summary>
    /// <typeparam name="T">
    /// The type that implement <see cref="ITemplateRepository"/>.
    /// </typeparam>
    /// <param name="builder">
    /// The builder to register to.
    /// </param>
    /// <returns>
    /// <paramref name="builder"/> to chain the call.
    /// </returns>
    public static ComposerBuilder AddTemplateRepository<T>(this ComposerBuilder builder)
        where T : class, ITemplateRepository
    {
        builder.Services.AddSingleton<ITemplateRepository, T>();

        return builder;
    }

    /// <summary>
    /// Register a new implementation of <see cref="IPropertyRenderer"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type that implement <see cref="IPropertyRenderer"/>.
    /// </typeparam>
    /// <param name="builder">
    /// The builder to register to.
    /// </param>
    /// <returns>
    /// <paramref name="builder"/> to chain the call.
    /// </returns>
    public static ComposerBuilder AddPropertyRenderer<T>(this ComposerBuilder builder)
        where T : class, IPropertyRenderer
    {
        builder.Services.AddSingleton<IPropertyRenderer, T>();

        return builder;
    }
}
