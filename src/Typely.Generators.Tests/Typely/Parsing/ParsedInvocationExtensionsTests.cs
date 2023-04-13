using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Tests.Typely.Parsing;

public class ParsedInvocationExtensionsTests
{
    [Fact]
    public void GetLambdaBodyOfFirstArgument_ShouldThrow_WhenUnsupportedArgumentExpressionType()
    {
        var syntaxTree = CSharpSyntaxTree.ParseText("MyMethod(42);");
        var root = syntaxTree.GetCompilationUnitRoot();
        var argumentListSyntax = root.DescendantNodes().OfType<InvocationExpressionSyntax>().First().ArgumentList;
        var invocation = new ParsedInvocation(argumentListSyntax, "");
        
        Assert.Throws<InvalidOperationException>(() => invocation.GetLambdaBodyOfFirstArgument());
    }
    
    [Fact]
    public void GetLambdaBodyOfFirstArgument_ShouldNotReplaceNonParameterVariables_ForASimpleLambdaExpressionSyntax()
    {
        var syntaxTree = CSharpSyntaxTree.ParseText("MyMethod(x => a == x);");
        var root = syntaxTree.GetCompilationUnitRoot();
        var argumentListSyntax = root.DescendantNodes().OfType<InvocationExpressionSyntax>().First().ArgumentList;
        var invocation = new ParsedInvocation(argumentListSyntax, "");

        Assert.Equal("a == value", invocation.GetLambdaBodyOfFirstArgument());
    }
    
    [Fact]
    public void GetLambdaBodyOfFirstArgument_ShouldNotReplaceNonParameterVariables_ForAParenthesizedLambdaExpressionSyntax()
    {
        var syntaxTree = CSharpSyntaxTree.ParseText("MyMethod((x) => a == x);");
        var root = syntaxTree.GetCompilationUnitRoot();
        var argumentListSyntax = root.DescendantNodes().OfType<InvocationExpressionSyntax>().First().ArgumentList;
        var invocation = new ParsedInvocation(argumentListSyntax, "");

        Assert.Equal("a == value", invocation.GetLambdaBodyOfFirstArgument());
    }
}