namespace Typely.Core;

public interface ITypelyValue<out T>
{
    T Value { get; }
}
