namespace Typely.Generators.Typely.Parsing;

/// <summary>
/// Contains the metadata of a rule to be emitted.
/// </summary>
internal class EmittableRuleBuilder
{
    /// <summary>
    /// A rule over the underlying value to emit.
    /// </summary>
    public string Rule { get; }

    /// <summary>
    /// Defines the error code associated to the message.
    /// </summary>
    public string ErrorCode { get; private set; }

    /// <summary>
    /// An error message to return when the rule fails.
    /// </summary>
    public string Message { get; private set; }

    /// <summary>
    /// Contains the list of variables and values that can be formatted into the error message.
    /// </summary>
    public Dictionary<string, string> PlaceholderValues { get; } = new();

    /// <summary>
    /// Private constructor.
    /// </summary>
    /// <param name="errorCode">The error code of the message.</param>
    /// <param name="rule">The rule to be converted to C# code.</param>
    /// <param name="message">A message to be converted to C# code.</param>
    private EmittableRuleBuilder(string errorCode, string rule, string message)
    {
        ErrorCode = errorCode;
        Rule = rule;
        Message = message;
    }

    /// <summary>
    /// Sets the error code of the rule.
    /// </summary>
    /// <param name="errorCode">The error code.</param>
    public void SetErrorCode(string errorCode)
    {
        ErrorCode = errorCode;
    }

    /// <summary>
    /// Sets the message of the rule.
    /// </summary>
    /// <param name="message">The message.</param>
    public void SetMessage(string message)
    {
        Message = message;
    }

    /// <summary>
    /// Creates a <see cref="EmittableRuleBuilder"/> with the default values that can be overridden.
    /// </summary>
    /// <param name="errorCode">The error code of the message.</param>
    /// <param name="rule">The rule to be converted to C# code.</param>
    /// <param name="message">A message to be converted to C# code.</param>
    /// <param name="placeholders">Key values used to format a custom message e.g. in the frontend.</param>
    /// <returns>A <see cref="EmittableRuleBuilder"/></returns>
    /// <remarks>It replaces variable with constants inside the rule.</remarks>
    public static EmittableRuleBuilder From(string errorCode, string rule, string message,
        params (string Key, string Value)[] placeholders)
    {
        var emittableRule = new EmittableRuleBuilder(errorCode, rule, message);
        foreach (var placeholder in placeholders)
        {
            emittableRule.PlaceholderValues.Add(placeholder.Key, placeholder.Value);
        }

        return emittableRule;
    }

    public EmittableRule Build() => new(ErrorCode, Rule, Message, PlaceholderValues);
}