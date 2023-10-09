using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Typely.Generators.Analysers;
using Typely.Generators.Tests.Typely.Specifications.Diagnostics;
using Typely.Generators.Typely;

namespace Typely.Generators.Tests.Analysers;

public class TypelySpecificationAnalyserTests
{
    [Fact]
    public async Task DetectUnsupportedMethods()
    {
        var diagnostics = await GetDiagnostics<UnsupportedMethodsSpecification>();

        diagnostics.ShouldContainExactlyNDiagnosticsWithId(1, DiagnosticDescriptors.TYP0003_UnsupportedMethod.Id);
    }

    [Fact]
    public async Task DetectUnsupportedProperties()
    {
        var diagnostics = await GetDiagnostics<UnsupportedPropertiesSpecification>();

        diagnostics.ShouldContainExactlyNDiagnosticsWithId(1, DiagnosticDescriptors.TYP0005_UnsupportedProperty.Id);
    }

    [Fact]
    public async Task DetectUnsupportedFields()
    {
        var diagnostics = await GetDiagnostics<UnsupportedFieldsSpecification>();

        diagnostics.ShouldContainExactlyNDiagnosticsWithId(2, DiagnosticDescriptors.TYP0004_UnsupportedField.Id);
    }

    [Fact]
    public async Task DetectUnsupportedTypes()
    {
        var diagnostics = await GetDiagnostics<UnsupportedTypesSpecification>();

        diagnostics.ShouldContainExactlyNDiagnosticsWithId(4, DiagnosticDescriptors.TYP0006_UnsupportedType.Id);
    }

    [Fact]
    public async Task DetectUnsupportedVariables()
    {
        var diagnostics = await GetDiagnostics<UnsupportedVariablesSpecification>();

        diagnostics.ShouldContainExactlyNDiagnosticsWithId(3, DiagnosticDescriptors.TYP0007_UnsupportedVariable.Id);
    }

    private async Task<ImmutableArray<Diagnostic>> GetDiagnostics<TSpec>() =>
        await AnalyserRunner.GetDiagnostics<TypelySpecificationAnalyser, TSpec>();
}