using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Tests.Typely.Parsing;

public class ParserTests
{
    [Fact]
    public void ParenthesizedDeclarationConfiguration_Should_Throw()
    {
        var context = new ParserContextFixture()
            .WithConfigurations(typeof(ParenthesizedDeclarationConfiguration))
            .Create();

        Assert.Throws<NotSupportedException>(() =>
            Parser.GetEmittableTypesForClass(context.ClassDeclarationSyntax, context.SemanticModel,
                CancellationToken.None));
    }

    [Fact]
    public void UnsupportedMemberNames_Should_Throw_ForBool()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelyConfiguration
            {
                public void Configure(ITypelyBuilder builder)
                {
                    builder.OfBool().WithUnsupportedMember();
                }
            }
            """);

        Assert.Throws<NotSupportedException>(() => GetEmittableTypes(syntaxTree));
    }

    [Fact]
    public void UnsupportedMemberNames_Should_Throw_ForByte()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelyConfiguration
            {
                public void Configure(ITypelyBuilder builder)
                {                    
                    builder.OfByte().WithUnsupportedMember();                   
                }
            }
            """);

        Assert.Throws<NotSupportedException>(() => GetEmittableTypes(syntaxTree));
    }

    [Fact]
    public void UnsupportedMemberNames_Should_Throw_ForChar()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelyConfiguration
            {
                public void Configure(ITypelyBuilder builder)
                {                   
                    builder.OfChar().WithUnsupportedMember();                  
                }
            }
            """);

        Assert.Throws<NotSupportedException>(() => GetEmittableTypes(syntaxTree));
    }

    [Fact]
    public void UnsupportedMemberNames_Should_Throw_ForDateOnly()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelyConfiguration
            {
                public void Configure(ITypelyBuilder builder)
                {                 
                    builder.OfDateOnly().WithUnsupportedMember();                  
                }
            }
            """);

        Assert.Throws<NotSupportedException>(() => GetEmittableTypes(syntaxTree));
    }

    [Fact]
    public void UnsupportedMemberNames_Should_Throw_ForDateTime()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelyConfiguration
            {
                public void Configure(ITypelyBuilder builder)
                {                   
                    builder.OfDateTime().WithUnsupportedMember();                   
                }
            }
            """);

        Assert.Throws<NotSupportedException>(() => GetEmittableTypes(syntaxTree));
    }

    [Fact]
    public void UnsupportedMemberNames_Should_Throw_ForDecimal()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelyConfiguration
            {
                public void Configure(ITypelyBuilder builder)
                {                   
                    builder.OfDecimal().WithUnsupportedMember();                   
                }
            }
            """);

        Assert.Throws<NotSupportedException>(() => GetEmittableTypes(syntaxTree));
    }

    [Fact]
    public void UnsupportedMemberNames_Should_Throw_ForDouble()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelyConfiguration
            {
                public void Configure(ITypelyBuilder builder)
                {                  
                    builder.OfDouble().WithUnsupportedMember();                 
                }
            }
            """);

        Assert.Throws<NotSupportedException>(() => GetEmittableTypes(syntaxTree));
    }

    [Fact]
    public void UnsupportedMemberNames_Should_Throw_ForFloat()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelyConfiguration
            {
                public void Configure(ITypelyBuilder builder)
                {                 
                    builder.OfFloat().WithUnsupportedMember();                  
                }
            }
            """);

        Assert.Throws<NotSupportedException>(() => GetEmittableTypes(syntaxTree));
    }

    [Fact]
    public void UnsupportedMemberNames_Should_Throw_ForGuid()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelyConfiguration
            {
                public void Configure(ITypelyBuilder builder)
                {                  
                    builder.OfGuid().WithUnsupportedMember();                 
                }
            }
            """);

        Assert.Throws<NotSupportedException>(() => GetEmittableTypes(syntaxTree));
    }

    [Fact]
    public void UnsupportedMemberNames_Should_Throw_ForInt()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelyConfiguration
            {
                public void Configure(ITypelyBuilder builder)
                {                   
                    builder.OfInt().WithUnsupportedMember();                   
                }
            }
            """);

        Assert.Throws<NotSupportedException>(() => GetEmittableTypes(syntaxTree));
    }

    [Fact]
    public void UnsupportedMemberNames_Should_Throw_ForLong()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelyConfiguration
            {
                public void Configure(ITypelyBuilder builder)
                {                 
                    builder.OfLong().WithUnsupportedMember();                   
                }
            }
            """);

        Assert.Throws<NotSupportedException>(() => GetEmittableTypes(syntaxTree));
    }

    [Fact]
    public void UnsupportedMemberNames_Should_Throw_ForSByte()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelyConfiguration
            {
                public void Configure(ITypelyBuilder builder)
                {                  
                    builder.OfSByte().WithUnsupportedMember();                   
                }
            }
            """);

        Assert.Throws<NotSupportedException>(() => GetEmittableTypes(syntaxTree));
    }

    [Fact]
    public void UnsupportedMemberNames_Should_Throw_ForShort()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelyConfiguration
            {
                public void Configure(ITypelyBuilder builder)
                {                   
                    builder.OfShort().WithUnsupportedMember();                  
                }
            }
            """);

        Assert.Throws<NotSupportedException>(() => GetEmittableTypes(syntaxTree));
    }

    [Fact]
    public void UnsupportedMemberNames_Should_Throw_ForString()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelyConfiguration
            {
                public void Configure(ITypelyBuilder builder)
                {                   
                    builder.OfString().WithUnsupportedMember();                  
                }
            }
            """);

        Assert.Throws<NotSupportedException>(() => GetEmittableTypes(syntaxTree));
    }

    [Fact]
    public void UnsupportedMemberNames_Should_Throw_ForTimeOnly()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelyConfiguration
            {
                public void Configure(ITypelyBuilder builder)
                {
                    builder.OfTimeOnly().WithUnsupportedMember();                   
                }
            }
            """);

        Assert.Throws<NotSupportedException>(() => GetEmittableTypes(syntaxTree));
    }

    [Fact]
    public void UnsupportedMemberNames_Should_Throw_ForUInt()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelyConfiguration
            {
                public void Configure(ITypelyBuilder builder)
                {
                    builder.OfUInt().WithUnsupportedMember();                  
                }
            }
            """);

        Assert.Throws<NotSupportedException>(() => GetEmittableTypes(syntaxTree));
    }

    [Fact]
    public void UnsupportedMemberNames_Should_Throw_ForULong()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelyConfiguration
            {
                public void Configure(ITypelyBuilder builder)
                {                    
                    builder.OfULong().WithUnsupportedMember();                   
                }
            }
            """);

        Assert.Throws<NotSupportedException>(() => GetEmittableTypes(syntaxTree));
    }

    [Fact]
    public void UnsupportedMemberNames_Should_Throw_ForUShort()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelyConfiguration
            {
                public void Configure(ITypelyBuilder builder)
                {                   
                    builder.OfUShort().WithUnsupportedMember();
                }
            }
            """);

        Assert.Throws<NotSupportedException>(() => GetEmittableTypes(syntaxTree));
    }

    [Fact]
    public void UnsupportedMemberNames_Should_Throw_ForTimeSpan()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelyConfiguration
            {
                public void Configure(ITypelyBuilder builder)
                {                   
                    builder.OfTimeSpan().WithUnsupportedMember();
                }
            }
            """);

        Assert.Throws<NotSupportedException>(() => GetEmittableTypes(syntaxTree));
    }

    [Fact]
    public void UnsupportedMemberNames_Should_Throw_ForDateTimeOffset()
    {
        var syntaxTree = SyntaxFactory.ParseSyntaxTree("""
            using Typely.Core;
            using Typely.Core.Builders;

            namespace Typely.Generators.Tests.Typely.Configurations;

            public class NotSupportedMember : ITypelyConfiguration
            {
                public void Configure(ITypelyBuilder builder)
                {                   
                    builder.OfDateTimeOffset().WithUnsupportedMember();
                }
            }
            """);

        Assert.Throws<NotSupportedException>(() => GetEmittableTypes(syntaxTree));
    }

    private static IReadOnlyList<EmittableType> GetEmittableTypes(SyntaxTree syntaxTree)
    {
        var context = new ParserContextFixture().WithSyntaxTrees(syntaxTree).Create();
        return Parser.GetEmittableTypesForClass(context.ClassDeclarationSyntax, context.SemanticModel,
            CancellationToken.None);
    }
}