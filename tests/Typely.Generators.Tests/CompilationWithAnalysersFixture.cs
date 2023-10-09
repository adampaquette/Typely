using AutoFixture;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Typely.Generators.Tests;

internal class CompilationWithAnalysersFixture : CompilationFixture<CompilationWithAnalysersFixture, CompilationWithAnalyzers>
{
    private ImmutableArray<DiagnosticAnalyzer> _diagnosticAnalyzers;
    
    public CompilationWithAnalysersFixture()
    {
        Fixture.Register(() => CreateCompilation().WithAnalyzers(_diagnosticAnalyzers));
    }
    
    public CompilationWithAnalysersFixture WithAnalysers(ImmutableArray<DiagnosticAnalyzer> diagnosticAnalyzers)
    {
        _diagnosticAnalyzers = diagnosticAnalyzers;
        return this;
    }
    
    public CompilationWithAnalysersFixture WithAnalyser<TAnalyser>() where TAnalyser : DiagnosticAnalyzer, new()
    {
        _diagnosticAnalyzers = ImmutableArray.Create<DiagnosticAnalyzer>(new TAnalyser());
        return this;
    }
}