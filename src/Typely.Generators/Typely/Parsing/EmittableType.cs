using System.Collections.Immutable;
using Typely.Generators.Comparers;

namespace Typely.Generators.Typely.Parsing;

internal class EmittableType : IEquatable<EmittableType>
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
    public string TypeName { get; }

    /// <summary>
    /// Name of the type used in error messages.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Namespace in witch to associate the value object.
    /// </summary>
    public string Namespace { get; }

    /// <summary>
    /// Namespace of the configuration class.
    /// </summary>
    public string ConfigurationNamespace { get; }

    /// <summary>
    /// Type kind to generate.
    /// </summary>
    public ConstructTypeKind ConstructTypeKind { get; }

    /// <summary>
    /// A function to normalize the value.
    /// </summary>
    public string? NormalizeFunctionBody { get; }

    /// <summary>
    /// A set of rules wich defines the value object.
    /// </summary>
    public ImmutableArray<EmittableRule> Rules { get; }

    /// <summary>
    /// Additional namespaces to import.
    /// </summary>
    public ImmutableArray<string> AdditionalNamespaces { get; }

    /// <summary>
    /// A set of dynamic properties. For example: MaxLength on string type.
    /// </summary>
    public TypeProperties Properties { get; }

    public EmittableType(string underlyingType, bool isValueType, string typeName, string name, string ns,
        string configurationNamespace, ConstructTypeKind constructTypeKind, string? normalizeFunctionBody,
        ImmutableArray<EmittableRule> rules, ImmutableArray<string> additionalNamespaces, TypeProperties properties)
    {
        UnderlyingType = underlyingType;
        IsValueType = isValueType;
        TypeName = typeName;
        Name = name;
        Namespace = ns;
        ConfigurationNamespace = configurationNamespace;
        ConstructTypeKind = constructTypeKind;
        NormalizeFunctionBody = normalizeFunctionBody;
        Rules = rules;
        AdditionalNamespaces = additionalNamespaces;
        Properties = properties;
    }

    public bool Equals(EmittableType? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return UnderlyingType == other.UnderlyingType &&
               IsValueType == other.IsValueType &&
               TypeName == other.TypeName &&
               Name == other.Name &&
               Namespace == other.Namespace &&
               ConfigurationNamespace == other.ConfigurationNamespace &&
               ConstructTypeKind == other.ConstructTypeKind &&
               NormalizeFunctionBody == other.NormalizeFunctionBody &&
               ImmutableArrayComparer<EmittableRule>.Default.Equals(Rules, other.Rules) &&
               ImmutableArrayComparer<string>.Default.Equals(AdditionalNamespaces, other.AdditionalNamespaces) &&
               DictionaryComparer<string, object>.Default.Equals(Properties, other.Properties);
    }

    public override bool Equals(object? obj) => Equals(obj as EmittableType);

    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = 17;

            hashCode = hashCode * 23 + UnderlyingType.GetHashCode();
            hashCode = hashCode * 23 + IsValueType.GetHashCode();
            hashCode = hashCode * 23 + TypeName.GetHashCode();
            hashCode = hashCode * 23 + Name.GetHashCode();
            hashCode = hashCode * 23 + Namespace.GetHashCode();
            hashCode = hashCode * 23 + ConfigurationNamespace.GetHashCode();
            hashCode = hashCode * 23 + ((int)ConstructTypeKind).GetHashCode();
            hashCode = hashCode * 23 + (NormalizeFunctionBody?.GetHashCode() ?? 0);
            hashCode = hashCode * 23 + ImmutableArrayComparer<EmittableRule>.Default.GetHashCode(Rules);
            hashCode = hashCode * 23 + ImmutableArrayComparer<string>.Default.GetHashCode(AdditionalNamespaces);
            hashCode = hashCode * 23 + DictionaryComparer<string, object>.Default.GetHashCode(Properties);

            return hashCode;
        }
    }
}