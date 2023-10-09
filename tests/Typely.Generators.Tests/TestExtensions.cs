using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace Typely.Generators.Tests;

public static class TestExtensions
{
    internal static void ShouldContainExactlyNDiagnosticsWithId(this ImmutableArray<Diagnostic> diagnostics, int n, string diagnosticId)
    {
        Assert.Equal(n, diagnostics.Count(x => x.Id == diagnosticId));
    }
}