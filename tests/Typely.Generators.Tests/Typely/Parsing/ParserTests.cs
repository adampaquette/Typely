using Microsoft.CodeAnalysis.CSharp;
using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Tests.Typely.Parsing;

public class ParserTests
{
    [Fact]
    public void ParenthesizedDeclarationConfiguration_Should_Throw()
    {
        var generatorClassContext = new GeneratorSyntaxContextFixture()
            .WithConfigurations(typeof(ParenthesizedDeclarationConfiguration))
            .Create();
        
        Assert.Throws<NotSupportedException>(() => Parser.GetEmittableTypesForClasses(generatorClassContext, CancellationToken.None));
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
        
        var generatorClassContext = new GeneratorSyntaxContextFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => Parser.GetEmittableTypesForClasses(generatorClassContext, CancellationToken.None));
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
        
        var generatorClassContext = new GeneratorSyntaxContextFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => Parser.GetEmittableTypesForClasses(generatorClassContext, CancellationToken.None));
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
        
        var generatorClassContext = new GeneratorSyntaxContextFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => Parser.GetEmittableTypesForClasses(generatorClassContext, CancellationToken.None));
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
        
        var generatorClassContext = new GeneratorSyntaxContextFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => Parser.GetEmittableTypesForClasses(generatorClassContext, CancellationToken.None));
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
        
        var generatorClassContext = new GeneratorSyntaxContextFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => Parser.GetEmittableTypesForClasses(generatorClassContext, CancellationToken.None));
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
        
        var generatorClassContext = new GeneratorSyntaxContextFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => Parser.GetEmittableTypesForClasses(generatorClassContext, CancellationToken.None));
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
        
        var generatorClassContext = new GeneratorSyntaxContextFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => Parser.GetEmittableTypesForClasses(generatorClassContext, CancellationToken.None));
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
        
        var generatorClassContext = new GeneratorSyntaxContextFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => Parser.GetEmittableTypesForClasses(generatorClassContext, CancellationToken.None));
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
        
        var generatorClassContext = new GeneratorSyntaxContextFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => Parser.GetEmittableTypesForClasses(generatorClassContext, CancellationToken.None));
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
        
        var generatorClassContext = new GeneratorSyntaxContextFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => Parser.GetEmittableTypesForClasses(generatorClassContext, CancellationToken.None));
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
        
        var generatorClassContext = new GeneratorSyntaxContextFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => Parser.GetEmittableTypesForClasses(generatorClassContext, CancellationToken.None));
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
        
        var generatorClassContext = new GeneratorSyntaxContextFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => Parser.GetEmittableTypesForClasses(generatorClassContext, CancellationToken.None));
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
        
        var generatorClassContext = new GeneratorSyntaxContextFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => Parser.GetEmittableTypesForClasses(generatorClassContext, CancellationToken.None));
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
        
        var generatorClassContext = new GeneratorSyntaxContextFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => Parser.GetEmittableTypesForClasses(generatorClassContext, CancellationToken.None));
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
        
        var generatorClassContext = new GeneratorSyntaxContextFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => Parser.GetEmittableTypesForClasses(generatorClassContext, CancellationToken.None));
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
        
        var generatorClassContext = new GeneratorSyntaxContextFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => Parser.GetEmittableTypesForClasses(generatorClassContext, CancellationToken.None));
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
        
        var generatorClassContext = new GeneratorSyntaxContextFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => Parser.GetEmittableTypesForClasses(generatorClassContext, CancellationToken.None));
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
        
        var generatorClassContext = new GeneratorSyntaxContextFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => Parser.GetEmittableTypesForClasses(generatorClassContext, CancellationToken.None));
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
        
        var generatorClassContext = new GeneratorSyntaxContextFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => Parser.GetEmittableTypesForClasses(generatorClassContext, CancellationToken.None));
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
        
        var generatorClassContext = new GeneratorSyntaxContextFixture().WithSyntaxTrees(syntaxTree).Create();
        Assert.Throws<NotSupportedException>(() => Parser.GetEmittableTypesForClasses(generatorClassContext, CancellationToken.None));
    }
}

