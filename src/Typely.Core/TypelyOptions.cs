namespace Typely.Core;

public class TypelyOptions
{
    private static readonly TypelyOptions? _instance = null;

    public static TypelyOptions Instance => _instance ?? new TypelyOptions();

    public TypelyOptions EnableSensitiveDataLogging(bool enabled)
    {
        IsSensitiveDataLoggingEnabled = enabled;
        return Instance;
    }
    
    public bool IsSensitiveDataLoggingEnabled { get; private set; }
}
