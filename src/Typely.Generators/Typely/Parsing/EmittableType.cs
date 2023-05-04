using System.Collections.Immutable;

namespace Typely.Generators.Typely.Parsing;

internal record EmittableType(string UnderlyingType, bool IsValueType, string? TypeName, string? Name, string Namespace,
    string ConfigurationNamespace, ConstructTypeKind ConstructTypeKind, string? NormalizeFunctionBody,
    ImmutableArray<EmittableRule> Rules, ImmutableArray<string> AdditionalNamespaces, TypeProperties Properties)
{
    /// <summary>
    /// Type wrapped by the value object.
    /// </summary>
    public string UnderlyingType { get; } = UnderlyingType;

    /// <summary>
    /// Indicates if the type is a value type.
    /// </summary>
    public bool IsValueType { get; } = IsValueType;

    /// <summary>
    /// Name of the class of struct to generate.
    /// </summary>
    public string? TypeName { get; } = TypeName;

    /// <summary>
    /// Name of the type used in error messages.
    /// </summary>
    public string? Name { get; } = Name;

    /// <summary>
    /// Namespace in witch to associate the value object.
    /// </summary>
    public string Namespace { get; } = Namespace;

    /// <summary>
    /// Namespace of the configuration class.
    /// </summary>
    public string ConfigurationNamespace { get; } = ConfigurationNamespace;

    /// <summary>
    /// Type kind to generate.
    /// </summary>
    public ConstructTypeKind ConstructTypeKind { get; } = ConstructTypeKind;

    /// <summary>
    /// A function to normalize the value.
    /// </summary>
    public string? NormalizeFunctionBody { get; } = NormalizeFunctionBody;

    /// <summary>
    /// A set of rules wich defines the value object.
    /// </summary>
    public ImmutableArray<EmittableRule> Rules { get; } = Rules;

    /// <summary>
    /// Additional namespaces to import.
    /// </summary>
    public ImmutableArray<string> AdditionalNamespaces { get; } = AdditionalNamespaces;

    /// <summary>
    /// A set of dynamic properties. For example: MaxLength on string type.
    /// </summary>
    public TypeProperties Properties { get; } = Properties;
}