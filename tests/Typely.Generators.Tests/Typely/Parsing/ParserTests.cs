using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Tests.Typely.Parsing;

public class ParserTests
{
    // [Fact]
    // public void ParenthesizedDeclarationConfiguration_Should_Throw()
    // {
    //     var context = new ParserContextFixture()
    //         .WithConfigurations(typeof(ParenthesizedDeclarationConfiguration))
    //         .Create();
    //
    //     Assert.Throws<NotSupportedException>(() =>
    //         Parser.GetEmittableTypesForClass(context.ClassDeclarationSyntax, context.SemanticModel,
    //             CancellationToken.None));
    // }

    [Fact]
    public void UnsupportedMemberNames_Should_Throw_ForBool()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelySpecification
            {
                public void Create(ITypelyBuilder builder)
                {
                    builder.OfBool().WithUnsupportedMember();
                    builder.OfBool().For("Type1");
                }
            }
            """);

        var emittableTypes = GetEmittableTypes(syntaxTree);
        Assert.Equal("Type1", emittableTypes.First().TypeName);
    }

    [Fact]
    public void SyntaxError_Should_SkipLine()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelySpecification
            {
                public void Create(ITypelyBuilder builder)
                {
                    builder.OfBool().For("Type1");
                    builder.OfBool().For("Type2").WithError
                    builder.OfBool().For("Type3");
                }
            }
            """);

        var emittableTypes = GetEmittableTypes(syntaxTree);

        Assert.Equal(2, emittableTypes.Count);
    }

    private static IReadOnlyList<EmittableType> GetEmittableTypes(SyntaxTree syntaxTree)
    {
        var context = new ParserContextFixture().WithSyntaxTrees(syntaxTree).Create();
        return Parser.GetEmittableTypesAndDiagnosticsForClass(context.ClassDeclarationSyntax, context.SemanticModel,
            CancellationToken.None)
            .Where(x => x.Diagnostic is not null)
            .Select(x => x.EmittableType!)
            .ToList();
    }
}