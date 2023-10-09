using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
// ReSharper disable InconsistentNaming

namespace Typely.Generators.Tests.Analysers;

internal static class AnalyserVerifier
{
    private const string CS5001_ProgramDoesNotContainValidEntryPointId = "CS5001";
    private const string CS0012_TypeIsDefinedInAnAssemblyThatIsNotReferenced = "CS0012";

    private static readonly string[] DisabledDiagnostics = {
        CS5001_ProgramDoesNotContainValidEntryPointId, 
        CS0012_TypeIsDefinedInAnAssemblyThatIsNotReferenced
    };

    public static async Task<ImmutableArray<Diagnostic>> GetDiagnostics<TAnalyser, TSpecification>()
        where TAnalyser : DiagnosticAnalyzer, new()
    {
        var compilation =
            new CompilationWithAnalysersFixture()
                .WithSpecification<TSpecification>()
                .WithAnalyser<TAnalyser>()
                .Create();
        
        var diagnostics = await compilation.GetAllDiagnosticsAsync();
        return diagnostics.Where(x =>  !DisabledDiagnostics.Contains(x.Id)).ToImmutableArray();
    }
}