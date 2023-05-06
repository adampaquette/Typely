using System.Collections.Immutable;
using Typely.Generators.Comparers;

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
    public ImmutableDictionary<string, object?> PlaceholderValues { get; }

    public EmittableRule(string errorCode, string rule, string message,
        ImmutableDictionary<string, object?> placeholderValues)
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
               DictionaryComparer<string, object?>.Default.Equals(PlaceholderValues, other.PlaceholderValues);
    }
    
    public override bool Equals(object? obj) => Equals(obj as EmittableRule);

    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 23 + ErrorCode.GetHashCode();
        hash = hash * 23 + Rule.GetHashCode();
        hash = hash * 23 + Message.GetHashCode();
        hash = hash * 23 + DictionaryComparer<string, object?>.Default.GetHashCode(PlaceholderValues);
        return hash;
    }
}