namespace Composer.StringTemplate;

using System;
using Antlr4.StringTemplate;

public interface IPropertyRenderer : IAttributeRenderer
{
    /// <summary>
    /// Gets the type of this renderer supported.
    /// </summary>
    Type TargetType { get; }
}
