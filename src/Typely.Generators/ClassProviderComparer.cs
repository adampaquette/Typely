using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Typely.Generators;

/// <summary>
/// Invalidates the source generator's cache only if the class syntax has changed.
/// </summary>
public class ClassProviderComparer : IEqualityComparer<(ClassDeclarationSyntax Syntax, Compilation Compilation)>
{
    public bool Equals(
        (ClassDeclarationSyntax Syntax, Compilation Compilation) x,
        (ClassDeclarationSyntax Syntax, Compilation Compilation) y)
    {
        return x.Syntax.Equals(y.Syntax);
    }

    public int GetHashCode((ClassDeclarationSyntax Syntax, Compilation Compilation) obj)
    {
        return obj.Syntax.GetHashCode();
    }
}