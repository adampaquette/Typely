using Typely.Core.Builders;

namespace Typely.Core;

/// <summary>
/// Gives an Api to configure the creation of value objects.
/// </summary>
public interface ITypelySpecification
{
    /// <summary>
    /// Define how to create value objects.
    /// </summary>
    /// <param name="builder">Builder used to create value objects.</param>
    void Create(ITypelyBuilder builder);
}