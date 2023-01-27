using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Typely.Generators.Typely.Parsing;

internal class InvocationResult
{
    /// <summary>
    /// Builder or local variable
    /// </summary>
    public string Root { get; set; } = string.Empty;

    public List<MemberAccess> MembersAccess { get; set; } = new();
}

internal class MemberAccess
{
    public string MemberName { get; set; }
    public ArgumentListSyntax ArgumentListSyntax { get; set; }

    public MemberAccess(ArgumentListSyntax argumentListSyntax, string memberName)
    {
        ArgumentListSyntax = argumentListSyntax;
        MemberName = memberName;
    }
}