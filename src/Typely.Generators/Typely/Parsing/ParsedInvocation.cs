using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Typely.Generators.Typely.Parsing;

internal class ParsedInvocation
{
    public string MemberName { get; set; }
    public ArgumentListSyntax ArgumentListSyntax { get; set; }

    public ParsedInvocation(ArgumentListSyntax argumentListSyntax, string memberName)
    {
        ArgumentListSyntax = argumentListSyntax;
        MemberName = memberName;
    }
}