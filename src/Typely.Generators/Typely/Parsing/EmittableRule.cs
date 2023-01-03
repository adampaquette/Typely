using System.Linq.Expressions;

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
    public Expression Rule { get; }

    /// <summary>
    /// An error message to return when the rule fails.
    /// </summary>
    public Expression<Func<string>> Message { get; set; }

    /// <summary>
    /// Contains the list of variables and values that can be formatted into the error message.
    /// </summary>
    public Dictionary<string, object?> PlaceholderValues { get; set; } = new Dictionary<string, object?>();

    /// <summary>
    /// Private constructor.
    /// </summary>
    /// <param name="errorCode">The error code of the message.</param>
    /// <param name="rule">The rule to be converted to C# code.</param>
    /// <param name="message">A message to be converted to C# code.</param>
    private EmittableRule(string errorCode, Expression rule, Expression<Func<string>> message)
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
    /// <returns>A <see cref="EmittableRule"/></returns>
    public static EmittableRule From(string errorCode, Expression rule, Expression<Func<string>> message) =>
        new EmittableRule(errorCode, rule, message);

    /// <summary>
    /// Creates a <see cref="EmittableRule"/> with the default values that can be overridden.
    /// </summary>
    /// <param name="errorCode">The error code of the message.</param>
    /// <param name="rule">The rule to be converted to C# code.</param>
    /// <param name="message">A message to be converted to C# code.</param>
    /// <returns>A <see cref="EmittableRule"/></returns>
    /// <remarks>It replaces variable with constants inside the rule.</remarks>
    public static EmittableRule From<TDelegate>(string errorCode, Expression<TDelegate> rule, Expression<Func<string>> message, params (string Key, object Value)[] placeholders)
    {
        var emittableRule = new EmittableRule(errorCode, rule.ReplaceVariablesWithConstants(), message);
        foreach (var placeholder in placeholders)
        {
            emittableRule.PlaceholderValues.Add(placeholder.Key, placeholder.Value);
        }

        return emittableRule;
    }
}