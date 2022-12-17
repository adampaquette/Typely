using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;

namespace FluentType.Generators.Extensions;
internal static class CSharpCompilationExtensions
{
    public static CSharpCompilation WithReferences(this CSharpCompilation compilation, params Type[] references)
    {
        var metadataRefs = references.Select(x => MetadataReference.CreateFromFile(x.Assembly.Location));
        return compilation.AddReferences(metadataRefs);
    }

    public static CSharpCompilation WithDefaultNetCoreReferences(this CSharpCompilation compilation)
    {
        var rtPath = Path.GetDirectoryName(typeof(object).Assembly.Location) + Path.DirectorySeparatorChar;

        //var metadataRefs = references.Select(x => MetadataReference.CreateFromFile(x.Assembly.Location));
        return compilation.AddReferences(null);
    }
}
