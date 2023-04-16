namespace Typely.Generators.Typely.Parsing;

/// <summary>
/// Contains metadata of a type to generate.
/// </summary>
internal class EmittableType
{
    /// <summary>
    /// Type wrapped by the value object.
    /// </summary>
    public string UnderlyingType { get; }
    
    /// <summary>
    /// Indicates if the type is a value type.
    /// </summary>
    public bool IsValueType { get; }

    /// <summary>
    /// Name of the class of struct to generate.
    /// </summary>
    public string? TypeName { get; private set; }

    /// <summary>
    /// Name of the type used in error messages.
    /// </summary>
    public string? Name { get; private set; }

    /// <summary>
    /// Namespace in witch to associate the value object.
    /// </summary>
    public string Namespace { get; private set; }

    /// <summary>
    /// Type kind to generate.
    /// </summary>
    public ConstructTypeKind ConstructTypeKind { get; private set; } = ConstructTypeKind.Struct;

    /// <summary>
    /// A set of rules wich defines the value object.
    /// </summary>
    public List<EmittableRule> Rules { get; } = new();

    /// <summary>
    /// Used internally to change the rule smoothly.
    /// </summary>
    public EmittableRule? CurrentRule { get; set; }

    /// <summary>
    /// Additional namespaces to import.
    /// </summary>
    public IList<string> AdditionalNamespaces { get; } = new List<string>();
    
    public EmittableType(string underlyingType, bool isValueType, string defaultNamespace)
    {
        UnderlyingType = underlyingType;
        IsValueType = isValueType;
        Namespace = defaultNamespace;
    }

    /// <summary>
    /// Sets the type's name.
    /// </summary>
    /// <param name="typeName">Name of the type.</param>
    public void SetTypeName(string typeName)
    {
        TypeName = typeName.Trim();
        Name ??= $"\"{TypeName}\"";
    }

    /// <summary>
    /// Sets the name of the type used in error messages.
    /// </summary>
    /// <param name="name">Name.</param>
    public void SetName(string name) => Name = name.Trim();

    /// <summary>
    /// Sets the namespace in witch to associate the value object.
    /// </summary>
    /// <param name="value">Namespace.</param>
    public void SetNamespace(string value) => Namespace = value.Trim();

    /// <summary>
    /// Sets the type as a class.
    /// </summary>
    public void AsClass() => ConstructTypeKind = ConstructTypeKind.Class;

    /// <summary>
    /// Sets the type as a struct.
    /// </summary>
    public void AsStruct() => ConstructTypeKind = ConstructTypeKind.Struct;

    /// <summary>
    /// Adds a rule to the type.
    /// </summary>
    /// <param name="emittableRule">Rule.</param>
    public void AddRule(EmittableRule emittableRule)
    {
        CurrentRule = emittableRule;
        Rules.Add(emittableRule);
    }

    /// <summary>
    /// Sets the error code for the last rule added.
    /// </summary>
    /// <param name="errorCode">Unique error code.</param>
    public void SetCurrentErrorCode(string errorCode) => CurrentRule!.ErrorCode = errorCode;

    /// <summary>
    /// Sets the error message for the last rule added.
    /// </summary>
    /// <param name="message">Error message.</param>
    public void SetCurrentMessage(string message) => CurrentRule!.Message = message;

    /// <summary>
    /// Clone the rule to make similar types.
    /// </summary>
    /// <returns>The copied rule.</returns>
    // public EmittableType Clone()
    // {
    //     var emittableType = new EmittableType(UnderlyingType, Namespace)
    //     {
    //         TypeName = TypeName,
    //         ConstructTypeKind = ConstructTypeKind,
    //         CurrentRule = CurrentRule,
    //         Name = Name
    //     };
    //
    //     foreach (var rule in Rules)
    //     {
    //         emittableType.Rules.Add(rule);
    //     }
    //
    //     return emittableType;
    // }
}