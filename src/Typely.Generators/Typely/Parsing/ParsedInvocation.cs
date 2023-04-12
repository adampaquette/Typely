using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace Typely.Generators.Typely.Parsing;

[DebuggerDisplay("{MemberName}({ArgumentListSyntax})")]
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