using System.Linq.Expressions;

namespace Typely.Core.Builders;

/// <summary>
/// Factory for creating similar value objects.
/// </summary>
/// <typeparam name="TBuilder">Type of the value object builder.</typeparam>
public interface ITypelyFactory<TBuilder>
{
    /// <summary>
    /// Sets the type to generate.
    /// </summary>
    /// <param name="typeName">Name of the type.</param>
    /// <returns>Fluent <see cref="TBuilder"/>.</returns>
    TBuilder For(string typeName);

    /// <summary>
    /// Makes the generated type a struct.
    /// </summary>
    /// <returns>Fluent <see cref="TBuilder"/>.</returns>
    TBuilder AsStruct();

    /// <summary>
    /// Makes the generated type a class.
    /// </summary>
    /// <returns>Fluent <see cref="TBuilder"/>.</returns>
    TBuilder AsClass();

    /// <summary>
    /// Sets the namespace associated to the generate type.
    /// </summary>
    /// <param name="value">Namespace</param>
    /// <returns>Fluent <see cref="TBuilder"/>.</returns>
    TBuilder WithNamespace(string value);

    /// <summary>
    /// Sets the name used in the validation error message.
    /// </summary>
    /// <param name="name">User friendly name for the type.</param>
    /// <returns>Fluent <see cref="TBuilder"/>.</returns>
    TBuilder WithName(string name);

    /// <summary>
    /// Sets the name used in the validation error message. Supports localization.
    /// </summary>
    /// <param name="expression">An expression to be evaluated at runtime to get the name.</param>
    /// <returns>Fluent <see cref="TBuilder"/>.</returns>
    TBuilder WithName(Expression<Func<string>> expression);
}