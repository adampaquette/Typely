using Microsoft.CodeAnalysis;

// ReSharper disable InconsistentNaming

namespace Typely.Generators.Typely;

public static class DiagnosticDescriptors
{
    public static DiagnosticDescriptor TYP0001_UnsupportedExpression { get; } = new(
        id: "TYP0001",
        title: "Unsupported expression",
        messageFormat: "The use of '{0}' is not supported in a TypelySpecification.",
        category: "Design",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static DiagnosticDescriptor TYP0002_UnsupportedParameter { get; } = new(
        id: "TYP0002",
        title: "Unsupported parameter",
        messageFormat:
        "'{0}' is not supported as a parameter of '{1}' in the TypelySpecification. Instead use a string constant.",
        category: "Design",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static DiagnosticDescriptor TYP0003_UnsupportedMethod { get; } = new(
        id: "TYP0003",
        title: "Unsupported method",
        messageFormat: "Custom methods are not supported in a TypelySpecification. Remove '{0}'.",
        category: "Design",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static DiagnosticDescriptor TYP0004_UnsupportedField { get; } = new(
        id: "TYP0004",
        title: "Unsupported field",
        messageFormat: "Custom fields are not supported in a TypelySpecification. Remove '{0}'.",
        category: "Design",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static DiagnosticDescriptor TYP0005_UnsupportedProperty { get; } = new(
        id: "TYP0005",
        title: "Unsupported property",
        messageFormat: "Custom properties are not supported in a TypelySpecification. Remove '{0}'.",
        category: "Design",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static DiagnosticDescriptor TYP0006_UnsupportedType { get; } = new(
        id: "TYP0006",
        title: "Unsupported type",
        messageFormat: "Custom types are not supported in a TypelySpecification. Remove '{0}'.",
        category: "Design",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public static DiagnosticDescriptor TYP0007_UnsupportedVariable { get; } = new(
        id: "TYP0007",
        title: "Unsupported variable",
        messageFormat: "Variable type '{0}' is not supported for '{1}' in a TypelySpecification.",
        category: "Design",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);
}