using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Typely.Generators.Extensions;

namespace Typely.Generators.Tests.Extensions;
public class ClassDeclarationSyntaxExtensionsTests
{
    [Fact]
    public void HasInterface_ShoudReturn_False_WhenNotBaseList()
    {
        var syntaxTree = CSharpSyntaxTree.ParseText("public class Test {}");
        var classSyntax = syntaxTree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().Single();

        Assert.False(classSyntax.HasInterface("ABC"));
    }
}
