using System.Linq.Expressions;

namespace Typely.Core.Builders;

/// <summary>
/// Factory for creating similar value objects.
/// </summary>
/// <typeparam name="TBuilder">Type of the value object builder.</typeparam>
public interface ITypelyFactory<TBuilder>
{
    TBuilder For(string typeName);
    TBuilder AsStruct();
    TBuilder AsClass();
    TBuilder WithNamespace(string value);
    TBuilder WithName(string name);
    TBuilder WithName(Expression<Func<string>> expression);
}