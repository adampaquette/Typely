using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace Typely.Generators.Tests;

public static class TestExtensions
{
    /// <summary>
    /// Asserts that the <paramref name="diagnostics"/> contain at least one diagnostic with <paramref name="diagnosticId"/>.
    /// </summary>
    internal static void ShouldContainDiagnosticWithId(this ImmutableArray<Diagnostic> diagnostics, string diagnosticId)
    {
        Assert.True(diagnostics.Any(x => x.Id == diagnosticId));
    }
    
    internal static void ShouldContainExactlyNDiagnosticsWithId(this ImmutableArray<Diagnostic> diagnostics, int n, string diagnosticId)
    {
        Assert.Equal(n, diagnostics.Count(x => x.Id == diagnosticId));
    }
}