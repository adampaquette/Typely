using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Typely.Generators.Tests.Typely.Parsing;

public class ParserTests
{
    [Fact]
    public void ParenthesizedDeclarationConfiguration_Should_Throw()
    {
        var fixture = new ParserFixture()
            .WithConfigurations(typeof(ParenthesizedDeclarationConfiguration));

        var parser = fixture.Create();
        var classSyntaxes = fixture.Compilation.SyntaxTrees
            .First()
            .GetRoot()
            .DescendantNodes()
            .OfType<ClassDeclarationSyntax>();

        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(classSyntaxes));
    }
}