using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Typely.Generators.Tests;

public class ClassProviderComparerTests
{
    private readonly CSharpCompilation _compilation = CSharpCompilation.Create("Test");

    private readonly ClassDeclarationSyntax _syntax = SyntaxFactory.ParseSyntaxTree("public class TestClass { }")
        .GetRoot()
        .DescendantNodes()
        .OfType<ClassDeclarationSyntax>()
        .First();

    [Fact]
    public void Equals_ShouldReturn_True()
    {
        var x = (_syntax, _compilation);
        var y = (_syntax, _compilation);

        var comparer = new ClassProviderComparer();
        var result = comparer.Equals(x, y);

        Assert.True(result);
    }

    [Fact]
    public void GetHashCode_ShouldReturn_True()
    {
        var x = (_syntax, _compilation);
        var y = (_syntax, _compilation);

        var comparer = new ClassProviderComparer();
        var result = comparer.GetHashCode(x) == comparer.GetHashCode(y);

        Assert.True(result);
    }
}