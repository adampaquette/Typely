using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Typely.SourceGenerators.Extensions;

internal static class ClassDeclarationSyntaxExtensions
{
    /// <summary>
    /// Indicates whether or not the class has a specific interface.
    /// </summary>
    public static bool HasInterface(this ClassDeclarationSyntax source, string interfaceName)
    {
        if (source?.BaseList == null)
        {
            return false;
        }

        return source.BaseList.Types.Any(x => x.ToString() == interfaceName);
    }
}
