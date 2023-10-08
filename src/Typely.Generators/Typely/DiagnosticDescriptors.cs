using Microsoft.CodeAnalysis;

namespace Typely.Generators.Typely;

public static class DiagnosticDescriptors
{
    public static DiagnosticDescriptor UnsupportedExpression { get; } = new(
        id: "TYP0001",
        title: "Unsupported expression",
        messageFormat: "The use of '{0}' is not allowed in a TypelySpecification",
        category: "Design",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);
}