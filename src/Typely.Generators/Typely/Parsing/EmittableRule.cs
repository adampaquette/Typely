namespace Typely.Generators.Typely.Parsing;

/// <summary>
/// Contains the metadata of a rule to be emitted.
/// </summary>
internal class EmittableRule
{
    /// <summary>
    /// Defines the error code associated to the message.
    /// </summary>
    public string ErrorCode { get; set; }

    /// <summary>
    /// A rule over the underlying value to emit.
    /// </summary>
    public string Rule { get; }

    /// <summary>
    /// An error message to return when the rule fails.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Contains the list of variables and values that can be formatted into the error message.
    /// </summary>
    public Dictionary<string, object?> PlaceholderValues { get; } = new();

    /// <summary>
    /// Private constructor.
    /// </summary>
    /// <param name="errorCode">The error code of the message.</param>
    /// <param name="rule">The rule to be converted to C# code.</param>
    /// <param name="message">A message to be converted to C# code.</param>
    private EmittableRule(string errorCode, string rule, string message)
    {
        ErrorCode = errorCode;
        Rule = rule;
        Message = message;
    }

    /// <summary>
    /// Creates a <see cref="EmittableRule"/> with the default values that can be overridden.
    /// </summary>
    /// <param name="errorCode">The error code of the message.</param>
    /// <param name="rule">The rule to be converted to C# code.</param>
    /// <param name="message">A message to be converted to C# code.</param>
    /// <param name="placeholders">Key values used to format a custom message e.g. in the frontend.</param>
    /// <returns>A <see cref="EmittableRule"/></returns>
    /// <remarks>It replaces variable with constants inside the rule.</remarks>
    public static EmittableRule From(string errorCode, string rule, string message, params (string Key, object Value)[] placeholders)
    {
        var emittableRule = new EmittableRule(errorCode, rule, message);
        foreach (var placeholder in placeholders)
        {
            emittableRule.PlaceholderValues.Add(placeholder.Key, placeholder.Value);
        }

        return emittableRule;
    }
}