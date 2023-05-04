using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Typely.Generators.Typely;

public record GeneratorClassContext(ClassDeclarationSyntax ClassDeclarationSyntax, SemanticModel SemanticModel)
{
    public ClassDeclarationSyntax ClassDeclarationSyntax { get; } = ClassDeclarationSyntax;
    public SemanticModel SemanticModel { get; } = SemanticModel;
}
