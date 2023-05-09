using System.Collections.Immutable;

namespace Typely.Generators.Typely.Parsing;

/// <summary>
/// Contains metadata of a type to generate.
/// </summary>
internal class EmittableTypeBuilder
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
    /// Namespace of the configuration class.
    /// </summary>
    public string ConfigurationNamespace { get; }

    /// <summary>
    /// Type kind to generate.
    /// </summary>
    public ConstructTypeKind? ConstructTypeKind { get; private set; }

    /// <summary>
    /// A function to normalize the value.
    /// </summary>
    public string? NormalizeFunctionBody { get; private set; }

    /// <summary>
    /// A set of rules wich defines the value object.
    /// </summary>
    public List<EmittableRuleBuilder> Rules { get; } = new();

    /// <summary>
    /// Used internally to change the rule smoothly.
    /// </summary>
    public EmittableRuleBuilder? CurrentRule { get; set; }

    /// <summary>
    /// Additional namespaces to import.
    /// </summary>
    public List<string> AdditionalNamespaces { get; } = new();

    /// <summary>
    /// A set of dynamic properties. For example: MaxLength on string type.
    /// </summary>
    public TypeProperties Properties { get; } = new();

    public EmittableTypeBuilder(string underlyingType, bool isValueType, string configurationNamespace)
    {
        UnderlyingType = underlyingType;
        IsValueType = isValueType;
        Namespace = configurationNamespace;
        ConfigurationNamespace = configurationNamespace;
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
    /// Sets the function to normalize the value.
    /// </summary>
    /// <param name="value">The normalizer function.</param>
    public void SetNormalizeFunction(string value) => NormalizeFunctionBody = value.Trim();

    /// <summary>
    /// Sets the type as a class.
    /// </summary>
    public void AsClass() => ConstructTypeKind = Parsing.ConstructTypeKind.Class;

    /// <summary>
    /// Sets the type as a struct.
    /// </summary>
    public void AsStruct() => ConstructTypeKind = Parsing.ConstructTypeKind.Struct;

    /// <summary>
    /// Adds a rule to the type.
    /// </summary>
    /// <param name="emittableRuleBuilder">Rule.</param>
    public void AddRule(EmittableRuleBuilder emittableRuleBuilder)
    {
        CurrentRule = emittableRuleBuilder;
        Rules.Add(emittableRuleBuilder);
    }

    /// <summary>
    /// Sets the error code for the last rule added.
    /// </summary>
    /// <param name="errorCode">Unique error code.</param>
    public void SetCurrentErrorCode(string errorCode) => CurrentRule!.SetErrorCode(errorCode);

    /// <summary>
    /// Sets the error message for the last rule added.
    /// </summary>
    /// <param name="message">Error message.</param>
    public void SetCurrentMessage(string message) => CurrentRule!.SetMessage(message);

    public EmittableType Build() => new(
        UnderlyingType,
        IsValueType,
        TypeName!,
        Name!,
        Namespace,
        ConfigurationNamespace,
        ConstructTypeKind ?? (IsValueType ? Parsing.ConstructTypeKind.Struct : Parsing.ConstructTypeKind.Class),
        NormalizeFunctionBody,
        Rules.Select(r => r.Build()).ToImmutableArray(),
        AdditionalNamespaces.ToImmutableArray(),
        Properties
    );
}