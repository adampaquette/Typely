namespace Typely.Generators.Typely.Parsing;

/// <summary>
/// Contains the metadata of a rule to be emitted.
/// </summary>
internal record EmittableRule(string ErrorCode, string Rule, string Message, IReadOnlyDictionary<string, object?> PlaceholderValues)
{
    /// <summary>
    /// Defines the error code associated to the message.
    /// </summary>
    public string ErrorCode { get; } = ErrorCode;

    /// <summary>
    /// A rule over the underlying value to emit.
    /// </summary>
    public string Rule { get; } = Rule;

    /// <summary>
    /// An error message to return when the rule fails.
    /// </summary>
    public string Message { get; } = Message;

    /// <summary>
    /// Contains the list of variables and values that can be formatted into the error message.
    /// </summary>
    public IReadOnlyDictionary<string, object?> PlaceholderValues { get; } = PlaceholderValues;
}