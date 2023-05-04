using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Typely.Generators.Typely;

public readonly struct GeneratorClassContext : IEquatable<GeneratorClassContext>
{
    public ClassDeclarationSyntax ClassDeclarationSyntax { get; }
    public SemanticModel SemanticModel { get; }

    public GeneratorClassContext(ClassDeclarationSyntax classDeclarationSyntax, SemanticModel semanticModel)
    {
        ClassDeclarationSyntax = classDeclarationSyntax;
        SemanticModel = semanticModel;
    }

    public bool Equals(GeneratorClassContext other)
    {
        return ClassDeclarationSyntax == other.ClassDeclarationSyntax;
    }

    public override bool Equals(object obj)
    {
        return obj is GeneratorClassContext other && Equals(other);
    }

    public override int GetHashCode()
    {
        return ClassDeclarationSyntax.GetHashCode();
    }
}