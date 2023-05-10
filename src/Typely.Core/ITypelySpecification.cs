using Typely.Core.Builders;

namespace Typely.Core;

/// <summary>
/// Represent a specification of how to create value objects. 
/// </summary>
/// <remarks>
/// The class will be parsed as text by the generator. You cannot create and use custom functions inside the class.
/// </remarks>
public interface ITypelySpecification
{
    /// <summary>
    /// Define how to create value objects.
    /// </summary>
    /// <param name="builder">Builder used to create value objects.</param>
    void Create(ITypelyBuilder builder);
}