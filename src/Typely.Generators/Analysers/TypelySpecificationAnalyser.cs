using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using Typely.Generators.Infrastructure;
using Typely.Generators.Typely;
using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Analysers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class TypelySpecificationAnalyser : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        ImmutableArray.Create(
            DiagnosticDescriptors.TYP0001_UnsupportedExpression,
            DiagnosticDescriptors.TYP0002_UnsupportedParameter,
            DiagnosticDescriptors.TYP0003_UnsupportedMethod,
            DiagnosticDescriptors.TYP0004_UnsupportedField,
            DiagnosticDescriptors.TYP0005_UnsupportedProperty,
            DiagnosticDescriptors.TYP0006_UnsupportedType,
            DiagnosticDescriptors.TYP0007_UnsupportedVariable);

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze |
                                               GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.RegisterCompilationStartAction(OnCompilationStart);
    }

    private static void OnCompilationStart(CompilationStartAnalysisContext context)
    {
        var typeCache = new TypeCache(context.Compilation);
        if (typeCache.ITypelySpecification == null)
        {
            return;
        }

        context.RegisterSymbolAction(ctx => AnalyseSymbol(ctx, typeCache),
            SymbolKind.NamedType);

        context.RegisterSyntaxNodeAction(ctx => AnalyzeSyntaxNode(ctx, typeCache),
            SyntaxKind.ClassDeclaration);
    }

    private static void AnalyseSymbol(SymbolAnalysisContext context, TypeCache typeCache)
    {
        var namedTypeSymbol = (INamedTypeSymbol)context.Symbol;
        if (!IsTypelySpecificationClass(namedTypeSymbol, typeCache.ITypelySpecification!))
        {
            return;
        }

        foreach (var member in namedTypeSymbol.GetMembers())
        {
            switch (member)
            {
                case IFieldSymbol symbol:
                    AnalyseFieldSymbol(context, symbol);
                    break;
                case IMethodSymbol symbol:
                    AnalyseMethodSymbol(context, symbol);
                    break;
                case INamedTypeSymbol symbol:
                    AnalyseNamedTypeSymbol(context, symbol);
                    break;
                case IPropertySymbol symbol:
                    AnalysePropertySymbol(context, symbol);
                    break;
            }
        }
    }

    private static void AnalysePropertySymbol(SymbolAnalysisContext context, IPropertySymbol property)
    {
        context.ReportDiagnostic(Diagnostic.Create(
            DiagnosticDescriptors.TYP0005_UnsupportedProperty,
            property.Locations.FirstOrDefault(),
            property.Name));
    }

    private static void AnalyseNamedTypeSymbol(SymbolAnalysisContext context, INamedTypeSymbol namedType)
    {
        context.ReportDiagnostic(Diagnostic.Create(
            DiagnosticDescriptors.TYP0006_UnsupportedType,
            namedType.Locations.FirstOrDefault(),
            namedType.Name));
    }

    private static void AnalyseMethodSymbol(SymbolAnalysisContext context, IMethodSymbol method)
    {
        if (!IsAllowedMethod(method))
        {
            context.ReportDiagnostic(Diagnostic.Create(
                DiagnosticDescriptors.TYP0003_UnsupportedMethod,
                method.Locations.FirstOrDefault(),
                method.Name));
        }

        bool IsAllowedMethod(IMethodSymbol method) =>
            method.MethodKind is MethodKind.Constructor or MethodKind.PropertyGet or MethodKind.PropertySet ||
            IsCreateMethod();

        bool IsCreateMethod() =>
            method is { MethodKind: MethodKind.Ordinary, Name: SymbolNames.CreateMethod };
    }

    private static void AnalyseFieldSymbol(SymbolAnalysisContext context, IFieldSymbol field)
    {
        context.ReportDiagnostic(Diagnostic.Create(
            DiagnosticDescriptors.TYP0004_UnsupportedField,
            field.Locations.FirstOrDefault(),
            field.Name));
    }

    private static void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context, TypeCache typeCache)
    {
        var classDeclaration = (ClassDeclarationSyntax)context.Node;

        if (!Parser.IsTypelySpecificationClass(classDeclaration))
        {
            return;
        }

        var createMethodDeclaration = classDeclaration.DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .FirstOrDefault(x => x.Identifier.Text == SymbolNames.CreateMethod);

        if (createMethodDeclaration is null)
        {
            return;
        }

        var localDeclarations = createMethodDeclaration.DescendantNodes().OfType<LocalDeclarationStatementSyntax>();
        var semanticModel = context.SemanticModel;

        foreach (var localDeclaration in localDeclarations)
        {
            foreach (var variable in localDeclaration.Declaration.Variables)
            {
                if (semanticModel.GetDeclaredSymbol(variable) is not ILocalSymbol localSymbol)
                {
                    continue;
                }

                if (!typeCache.SupportedVariablesTypes.Contains(localSymbol.Type, SymbolEqualityComparer.Default))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        DiagnosticDescriptors.TYP0007_UnsupportedVariable,
                        localSymbol.Locations.FirstOrDefault(),
                        localSymbol.Type,
                        localSymbol.Name));
                }
            }
        }
    }

    private static bool IsTypelySpecificationClass(INamedTypeSymbol type, INamedTypeSymbol itypelySpecification)
    {
        return type.TypeKind == TypeKind.Class &&
               !type.IsStatic &&
               type.Implements(itypelySpecification);
    }

    private sealed class TypeCache
    {
        public TypeCache(Compilation compilation)
        {
            ITypelySpecification = compilation.GetTypeByMetadataName(SymbolNames.ITypelySpecification);
            SupportedVariablesTypes = new[]
            {
                compilation.GetTypeByMetadataName(SymbolNames.ITypelyBuilderOfBool),
                compilation.GetTypeByMetadataName(SymbolNames.ITypelyBuilderOfByte),
                compilation.GetTypeByMetadataName(SymbolNames.ITypelyBuilderOfChar),
                compilation.GetTypeByMetadataName(SymbolNames.ITypelyBuilderOfDateOnly),
                compilation.GetTypeByMetadataName(SymbolNames.ITypelyBuilderOfDateTime),
                compilation.GetTypeByMetadataName(SymbolNames.ITypelyBuilderOfDateTimeOffset),
                compilation.GetTypeByMetadataName(SymbolNames.ITypelyBuilderOfInt),
                compilation.GetTypeByMetadataName(SymbolNames.ITypelyBuilderOfDecimal),
                compilation.GetTypeByMetadataName(SymbolNames.ITypelyBuilderOfFloat),
                compilation.GetTypeByMetadataName(SymbolNames.ITypelyBuilderOfGuid),
                compilation.GetTypeByMetadataName(SymbolNames.ITypelyBuilderOfDouble),
                compilation.GetTypeByMetadataName(SymbolNames.ITypelyBuilderOfLong),
                compilation.GetTypeByMetadataName(SymbolNames.ITypelyBuilderOfSByte),
                compilation.GetTypeByMetadataName(SymbolNames.ITypelyBuilderOfShort),
                compilation.GetTypeByMetadataName(SymbolNames.ITypelyBuilderOfString),
                compilation.GetTypeByMetadataName(SymbolNames.ITypelyBuilderOfTimeOnly),
                compilation.GetTypeByMetadataName(SymbolNames.ITypelyBuilderOfTimeSpan),
                compilation.GetTypeByMetadataName(SymbolNames.ITypelyBuilderOfUInt),
                compilation.GetTypeByMetadataName(SymbolNames.ITypelyBuilderOfULong),
                compilation.GetTypeByMetadataName(SymbolNames.ITypelyBuilderOfUShort),
                compilation.GetTypeByMetadataName(SymbolNames.IRuleBuilderOfBool),
                compilation.GetTypeByMetadataName(SymbolNames.IRuleBuilderOfByte),
                compilation.GetTypeByMetadataName(SymbolNames.IRuleBuilderOfChar),
                compilation.GetTypeByMetadataName(SymbolNames.IRuleBuilderOfDateOnly),
                compilation.GetTypeByMetadataName(SymbolNames.IRuleBuilderOfDateTime),
                compilation.GetTypeByMetadataName(SymbolNames.IRuleBuilderOfDateTimeOffset),
                compilation.GetTypeByMetadataName(SymbolNames.IRuleBuilderOfInt),
                compilation.GetTypeByMetadataName(SymbolNames.IRuleBuilderOfDecimal),
                compilation.GetTypeByMetadataName(SymbolNames.IRuleBuilderOfFloat),
                compilation.GetTypeByMetadataName(SymbolNames.IRuleBuilderOfGuid),
                compilation.GetTypeByMetadataName(SymbolNames.IRuleBuilderOfDouble),
                compilation.GetTypeByMetadataName(SymbolNames.IRuleBuilderOfLong),
                compilation.GetTypeByMetadataName(SymbolNames.IRuleBuilderOfSByte),
                compilation.GetTypeByMetadataName(SymbolNames.IRuleBuilderOfShort),
                compilation.GetTypeByMetadataName(SymbolNames.IRuleBuilderOfString),
                compilation.GetTypeByMetadataName(SymbolNames.IRuleBuilderOfTimeOnly),
                compilation.GetTypeByMetadataName(SymbolNames.IRuleBuilderOfTimeSpan),
                compilation.GetTypeByMetadataName(SymbolNames.IRuleBuilderOfUInt),
                compilation.GetTypeByMetadataName(SymbolNames.IRuleBuilderOfULong),
                compilation.GetTypeByMetadataName(SymbolNames.IRuleBuilderOfUShort),
                compilation.GetTypeByMetadataName(SymbolNames.IFactoryOfBool),
                compilation.GetTypeByMetadataName(SymbolNames.IFactoryOfByte),
                compilation.GetTypeByMetadataName(SymbolNames.IFactoryOfChar),
                compilation.GetTypeByMetadataName(SymbolNames.IFactoryOfDateOnly),
                compilation.GetTypeByMetadataName(SymbolNames.IFactoryOfDateTime),
                compilation.GetTypeByMetadataName(SymbolNames.IFactoryOfDateTimeOffset),
                compilation.GetTypeByMetadataName(SymbolNames.IFactoryOfInt),
                compilation.GetTypeByMetadataName(SymbolNames.IFactoryOfDecimal),
                compilation.GetTypeByMetadataName(SymbolNames.IFactoryOfFloat),
                compilation.GetTypeByMetadataName(SymbolNames.IFactoryOfGuid),
                compilation.GetTypeByMetadataName(SymbolNames.IFactoryOfDouble),
                compilation.GetTypeByMetadataName(SymbolNames.IFactoryOfLong),
                compilation.GetTypeByMetadataName(SymbolNames.IFactoryOfSByte),
                compilation.GetTypeByMetadataName(SymbolNames.IFactoryOfShort),
                compilation.GetTypeByMetadataName(SymbolNames.IFactoryOfString),
                compilation.GetTypeByMetadataName(SymbolNames.IFactoryOfTimeOnly),
                compilation.GetTypeByMetadataName(SymbolNames.IFactoryOfTimeSpan),
                compilation.GetTypeByMetadataName(SymbolNames.IFactoryOfUInt),
                compilation.GetTypeByMetadataName(SymbolNames.IFactoryOfULong),
                compilation.GetTypeByMetadataName(SymbolNames.IFactoryOfUShort)
            };
        }

        public INamedTypeSymbol? ITypelySpecification { get; }
        public INamedTypeSymbol?[] SupportedVariablesTypes { get; }
    }
}