using Microsoft.CodeAnalysis;

namespace Typely.Generators.Typely;

public static class DiagnosticDescriptors
{
    public static DiagnosticDescriptor UnsupportedExpression { get; } = new(
        id: "TYP0001",
        title: "Unsupported expression",
        messageFormat: "The use of '{0}' is not allowed in a TypelySpecification.",
        category: "Design",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);
    
    public static DiagnosticDescriptor UnsupportedParameter { get; } = new(
        id: "TYP0002",
        title: "Unsupported parameter",
        messageFormat: "'{0}' is not allowed as a parameter of '{1}' in the TypelySpecification. Instead use a string constant.",
        category: "Design",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);
}