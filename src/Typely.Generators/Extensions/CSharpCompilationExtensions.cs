using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Typely.Generators.Extensions;

/// <summary>
/// Extensions over <see cref="CSharpCompilation"/>.
/// </summary>
internal static class CSharpCompilationExtensions
{
    public static CSharpCompilation AddReferences(this CSharpCompilation compilation, params Type[] references) =>
        compilation.AddReferences(references.Select(x => MetadataReference.CreateFromFile(x.Assembly.Location)));
}