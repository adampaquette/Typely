namespace Typely.Core;

/// <summary>
/// Options to modify the Typely's value object at runtime.
/// </summary>
public class TypelyOptions
{
    private static readonly TypelyOptions? _instance = null;

    public static TypelyOptions Instance => _instance ?? new TypelyOptions();

    /// <summary>
    /// Add the actual value to the exceptions when thrown. 
    /// </summary>
    /// <param name="enabled">Output the actual value when set to <see langword="true" />.</param>
    /// <returns>Fluent <see cref="TypelyOptions"/></returns>
    public TypelyOptions EnableSensitiveDataLogging(bool enabled)
    {
        IsSensitiveDataLoggingEnabled = enabled;
        return Instance;
    }

    /// <summary>
    /// <see langword="true" /> if the actual value being validated will be thrown in exceptions.
    /// </summary>
    public bool IsSensitiveDataLoggingEnabled { get; private set; }
}
