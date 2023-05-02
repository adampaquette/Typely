using Microsoft.CodeAnalysis;

namespace Typely.Generators;

internal static class DiagnosticDescriptors
{
    public static DiagnosticDescriptor TypeNameMissing { get; } = new(
        id: "TYPLY0001",
        title: "Type name missing.",
        messageFormat: "A type is declared without a name in the namespace '{0}'.",
        category: "TypelyGenerator",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);
    
    public static DiagnosticDescriptor NameMissing { get; } = new(
        id: "TYPLY0002",
        title: "Name missing.",
        messageFormat: "A type is declared without a user friendly name in the namespace '{0}'.",
        category: "TypelyGenerator",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);
}