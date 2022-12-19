using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;

namespace FluentType.Generators.Extensions;
internal static class CSharpCompilationExtensions
{
    public static CSharpCompilation AddReferences(this CSharpCompilation compilation, params Type[] references) =>
        compilation.AddReferences(references.Select(x => MetadataReference.CreateFromFile(x.Assembly.Location)));
}
