using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Typely.Generators.Typely.Parsing;

internal class SyntaxInvocationResult
{
    /// <summary>
    /// Builder or local variable
    /// </summary>
    public string? Root { get; set; }

    public List<(string MemberName, ArgumentListSyntax ArgumentListSyntax)> MemberAccess { get; } = new();
}
