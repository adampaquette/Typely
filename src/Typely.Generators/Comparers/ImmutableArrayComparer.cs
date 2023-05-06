using System.Collections.Immutable;

namespace Typely.Generators.Comparers;

public class ImmutableArrayComparer<T> : IEqualityComparer<ImmutableArray<T>>
{
    public static readonly ImmutableArrayComparer<T> Default = new();

    private readonly IEqualityComparer<T> _valueComparer;

    private ImmutableArrayComparer(IEqualityComparer<T>? valueComparer = null)
    {
        _valueComparer = valueComparer ?? EqualityComparer<T>.Default;
    }

    public bool Equals(ImmutableArray<T> arr1, ImmutableArray<T> arr2)
    {
        if (arr1.Length != arr2.Length)
        {
            return false;
        }

        foreach (var value in arr1)
        {
            if (!arr2.Contains(value))
            {
                return false;
            }
        }

        return true;
    }

    public int GetHashCode(ImmutableArray<T> arr)
    {
        int hash = 1;

        foreach (var value in arr)
        {
            hash = hash * 31 + _valueComparer.GetHashCode(value);
        }

        return hash;
    }
}