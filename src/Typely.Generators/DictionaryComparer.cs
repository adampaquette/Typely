namespace Typely.Generators;

public class DictionaryComparer<TKey, TValue> : IEqualityComparer<IDictionary<TKey, TValue>>
    where TKey : notnull
{
    public static readonly DictionaryComparer<TKey, TValue> Default = new();

    private readonly IEqualityComparer<TValue> _valueComparer;

    private DictionaryComparer(IEqualityComparer<TValue>? valueComparer = null)
    {
        _valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
    }

    public bool Equals(IDictionary<TKey, TValue>? dict1, IDictionary<TKey, TValue>? dict2)
    {
        if (ReferenceEquals(dict1, dict2)) return true;
        if (dict1 is null || dict2 is null) return false;
        if (dict1.Count != dict2.Count) return false;

        foreach (var keyValuePair in dict1)
        {
            if (!dict2.TryGetValue(keyValuePair.Key, out var value)) return false;
            if (!_valueComparer.Equals(keyValuePair.Value, value)) return false;
        }

        return true;
    }

    public int GetHashCode(IDictionary<TKey, TValue>? obj)
    {
        if (obj == null)
        {
            return 0;
        }

        int hash = 1;
        int keyHash = 0;
        int valueHash = 0;

        foreach (var pair in obj)
        {
            keyHash += EqualityComparer<TKey>.Default.GetHashCode(pair.Key);
            valueHash += _valueComparer.GetHashCode(pair.Value);
        }

        hash = hash * 31 + keyHash;
        hash = hash * 31 + valueHash;

        return hash;
    }
}