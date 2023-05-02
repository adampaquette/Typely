using Microsoft.CodeAnalysis.CSharp;
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
        var classSyntaxe = fixture.Compilation.SyntaxTrees
            .First()
            .GetRoot()
            .DescendantNodes()
            .OfType<ClassDeclarationSyntax>()
            .First();

        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(classSyntaxe.SyntaxTree));
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
        
        var parser = new ParserFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(syntaxTree));
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
        
        var parser = new ParserFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(syntaxTree));
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
        
        var parser = new ParserFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(syntaxTree));
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
        
        var parser = new ParserFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(syntaxTree));
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
        
        var parser = new ParserFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(syntaxTree));
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
        
        var parser = new ParserFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(syntaxTree));
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
        
        var parser = new ParserFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(syntaxTree));
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
        
        var parser = new ParserFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(syntaxTree));
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
        
        var parser = new ParserFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(syntaxTree));
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
        
        var parser = new ParserFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(syntaxTree));
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
        
        var parser = new ParserFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(syntaxTree));
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
        
        var parser = new ParserFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(syntaxTree));
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
        
        var parser = new ParserFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(syntaxTree));
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
        
        var parser = new ParserFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(syntaxTree));
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
        
        var parser = new ParserFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(syntaxTree));
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
        
        var parser = new ParserFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(syntaxTree));
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
        
        var parser = new ParserFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(syntaxTree));
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
        
        var parser = new ParserFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(syntaxTree));
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
        
        var parser = new ParserFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(syntaxTree));
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
        
        var parser = new ParserFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => parser.GetEmittableTypes(syntaxTree));
    }
}

