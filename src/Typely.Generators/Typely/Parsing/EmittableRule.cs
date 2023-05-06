using System.Collections.Immutable;

namespace Typely.Generators.Typely.Parsing;

/// <summary>
/// Contains the metadata of a rule to be emitted.
/// </summary>
internal class EmittableRule : IEquatable<EmittableRule>
{
    /// <summary>
    /// Defines the error code associated to the message.
    /// </summary>
    public string ErrorCode { get; }

    /// <summary>
    /// A rule over the underlying value to emit.
    /// </summary>
    public string Rule { get; }

    /// <summary>
    /// An error message to return when the rule fails.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Contains the list of variables and values that can be formatted into the error message.
    /// </summary>
    public IReadOnlyDictionary<string, string> PlaceholderValues { get; }

    public EmittableRule(string errorCode, string rule, string message,
        IReadOnlyDictionary<string, string> placeholderValues)
    {
        ErrorCode = errorCode;
        Rule = rule;
        Message = message;
        PlaceholderValues = placeholderValues;
    }

    public bool Equals(EmittableRule? other)
    {
        if (other == null)
        {
            return false;
        }

        return ErrorCode == other.ErrorCode &&
               Rule == other.Rule &&
               Message == other.Message &&
               DictionaryComparer<string, string>.Default.Equals(PlaceholderValues, other.PlaceholderValues);
    }
    
    private bool DictionaryEquals(IReadOnlyDictionary<string, string> dict1, IReadOnlyDictionary<string, string> dict2)
    {
        if (Equals(dict1, dict2))
        {
            return true;
        }

        if (dict1.Count != dict2.Count)
        {
            return false;
        }

        foreach (var keyValuePair in dict1)
        {
            if (!dict2.TryGetValue(keyValuePair.Key, out var value)) return false;
            if (keyValuePair.Value != value) return false;
        }

        return true;
    }

    public override bool Equals(object? obj) => Equals(obj as EmittableRule);

    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 23 + ErrorCode.GetHashCode();
        hash = hash * 23 + Rule.GetHashCode();
        hash = hash * 23 + Message.GetHashCode();
        hash = hash * 23 + PlaceholderValues.GetHashCode();
        return hash;
    }
}