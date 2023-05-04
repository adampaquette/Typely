using Microsoft.CodeAnalysis;
using Typely.Generators.Typely.Emitting;
using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Tests.Typely.Emitting;

public class EmitterTests
{
    [Fact]
    public void Emitter_Should_OutputDiagnostics_When_TypeNameIsNull()
    {
        var diagnostics = new List<Diagnostic>();
        var emittableType = new EmittableTypeBuilder("int", true, "namespace");
        emittableType.SetName("aa");
        Emitter.Emit(emittableType.Build(), CancellationToken.None);
        Assert.NotEmpty(diagnostics);
    }
    
    [Fact]
    public void Emitter_Should_OutputDiagnostics_When_NameIsNull()
    {
        var diagnostics = new List<Diagnostic>();
        var emitter = new Emitter(diagnostic => diagnostics.Add(diagnostic), CancellationToken.None);
        var emittableType = new EmittableTypeBuilder("int", true, "namespace");
        emitter.Emit(emittableType.Build());
        Assert.NotEmpty(diagnostics);
    }
}