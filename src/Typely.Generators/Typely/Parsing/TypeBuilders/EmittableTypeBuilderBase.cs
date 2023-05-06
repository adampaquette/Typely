using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Typely.Generators.Typely.Emitting;

namespace Typely.Generators.Typely.Parsing.TypeBuilders;

internal class EmittableTypeBuilderBase
{
    protected readonly IEnumerable<ParsedInvocation> Invocations;
    protected readonly EmittableTypeBuilder EmittableTypeBuilder;
    private readonly SemanticModel _model;

    protected EmittableTypeBuilderBase(IEnumerable<ParsedInvocation> invocations, EmittableTypeBuilder emittableTypeBuilder,
        SemanticModel model)
    {
        Invocations = invocations;
        EmittableTypeBuilder = emittableTypeBuilder;
        _model = model;
    }

    public virtual EmittableType? Build()
    {
        foreach (var invocation in Invocations)
        {
            TryHandleInvocation(invocation);
        }

        return EmittableTypeBuilder.TypeName == null ? null : EmittableTypeBuilder.Build();
    }
    
    protected bool TryHandleInvocation(ParsedInvocation invocation) =>
        TryHandleCommonInvocation(invocation) ||
        TryHandleValidationInvocation(invocation) ||
        TryHandleIComparableInvocation(invocation);

    private bool TryHandleCommonInvocation(ParsedInvocation invocation)
    {
        switch (invocation.MemberName)
        {
            case TypelyBuilderOf.ForMethodName:
                EmittableTypeBuilder.SetTypeName(invocation.GetFirstStringArgument());
                return true;
            case TypelyBuilderOf.AsClassMethodName:
                EmittableTypeBuilder.AsClass();
                return true;
            case TypelyBuilderOf.AsStructMethodName:
                EmittableTypeBuilder.AsStruct();
                return true;
            case TypelyBuilderOf.WithNameMethodName:
                if (invocation.GetFirstExpressionArgument() is ParenthesizedLambdaExpressionSyntax lambdaExpression1)
                {
                    var expNamespace = GetContainingNamespace(lambdaExpression1);
                    EmittableTypeBuilder.AdditionalNamespaces.Add(expNamespace!);
                    EmittableTypeBuilder.SetName(lambdaExpression1.Body.ToString());
                }
                else
                {
                    EmittableTypeBuilder.SetName($"\"{invocation.GetFirstStringArgument()}\"");
                }

                return true;
            case TypelyBuilderOf.WithNamespaceMethodName:
                EmittableTypeBuilder.SetNamespace(invocation.GetFirstStringArgument());
                return true;
            case TypelyBuilderOf.NormalizeMethodName:
                var body = invocation.GetFirstExpressionArgument() as AnonymousFunctionExpressionSyntax;
                if (body != null)
                {
                    var namespaces = GetNamespaces(body.Body);
                    EmittableTypeBuilder.AdditionalNamespaces.AddRange(namespaces!);
                }

                var value = invocation.GetLambdaBodyOfFirstArgument();
                EmittableTypeBuilder.SetNormalizeFunction(value);
                return true;
            case TypelyBuilderOf.AsFactoryMethodName:
                //TODO : Support as factory to reflect code
                return true;
            default: return false;
        }
    }

    private bool TryHandleValidationInvocation(ParsedInvocation invocation)
    {
        switch (invocation.MemberName)
        {
            case TypelyBuilderOf.NotEmptyMethodName:
                AddRule(
                    errorCode: ErrorCodes.NotEmpty,
                    rule: $"{Emitter.ValueParameterName} == default",
                    message: ErrorMessageResource.NotEmpty);
                return true;
            case TypelyBuilderOf.NotEqualMethodName:
                var value = invocation.GetFirstArgument();
                AddRule(
                    errorCode: ErrorCodes.NotEqual,
                    rule: $"{Emitter.ValueParameterName}.Equals({value})",
                    message: ErrorMessageResource.NotEqual,
                    placeholders: (ValidationPlaceholder.ComparisonValue, value));
                return true;
            case TypelyBuilderOf.MustMethodName:
                value = invocation.GetLambdaBodyOfFirstArgument();
                AddRule(
                    errorCode: ErrorCodes.Must,
                    rule: $"!({value})",
                    message: ErrorMessageResource.Must);
                return true;
            case TypelyBuilderOf.WithMessageMethodName:
                if (invocation.GetFirstExpressionArgument() is ParenthesizedLambdaExpressionSyntax lambdaExpression2)
                {
                    var expNamespace = GetContainingNamespace(lambdaExpression2);
                    EmittableTypeBuilder.AdditionalNamespaces.Add(expNamespace!);
                    EmittableTypeBuilder.SetCurrentMessage(lambdaExpression2.Body.ToString());
                }
                else
                {
                    EmittableTypeBuilder.SetCurrentMessage($"\"{invocation.GetFirstStringArgument()}\"");
                }

                return true;
            case TypelyBuilderOf.WithErrorCodeMethodName:
                EmittableTypeBuilder.SetCurrentErrorCode(invocation.GetFirstStringArgument());
                return true;
            default: return false;
        }
    }

    private bool TryHandleIComparableInvocation(ParsedInvocation invocation)
    {
        switch (invocation.MemberName)
        {
            case TypelyBuilderOf.GreaterThanMethodName:
                var value = invocation.GetFirstArgument();
                AddRule(
                    errorCode: ErrorCodes.GreaterThan,
                    rule: $"{Emitter.ValueParameterName} <= {value}",
                    message: ErrorMessageResource.GreaterThan,
                    placeholders: (ValidationPlaceholder.ComparisonValue, value));
                return true;
            case TypelyBuilderOf.GreaterThanOrEqualToMethodName:
                value = invocation.GetFirstArgument();
                AddRule(
                    errorCode: ErrorCodes.GreaterThanOrEqualTo,
                    rule: $"{Emitter.ValueParameterName} < {value}",
                    message: ErrorMessageResource.GreaterThanOrEqualTo,
                    placeholders: (ValidationPlaceholder.ComparisonValue, value));
                return true;
            case TypelyBuilderOf.LessThanMethodName:
                value = invocation.GetFirstArgument();
                AddRule(
                    errorCode: ErrorCodes.LessThan,
                    rule: $"{Emitter.ValueParameterName} >= {value}",
                    message: ErrorMessageResource.LessThan,
                    placeholders: (ValidationPlaceholder.ComparisonValue, value));
                return true;
            case TypelyBuilderOf.LessThanOrEqualToMethodName:
                value = invocation.GetFirstArgument();
                AddRule(
                    errorCode: ErrorCodes.LessThanOrEqualTo,
                    rule: $"{Emitter.ValueParameterName} > {value}",
                    message: ErrorMessageResource.LessThanOrEqualTo,
                    placeholders: (ValidationPlaceholder.ComparisonValue, value));
                return true;
            default: return false;
        }
    }

    private IEnumerable<string> GetNamespaces(CSharpSyntaxNode expressionSyntax) => expressionSyntax
        .DescendantNodes()
        .SelectMany(node => NamespaceResolver.GetNamespacesFromLambda(node, _model))
        .Distinct()
        .Where(x => x != EmittableTypeBuilder.ConfigurationNamespace);

    private string? GetContainingNamespace(ParenthesizedLambdaExpressionSyntax lambdaExpression) =>
        ModelExtensions.GetSymbolInfo(_model, lambdaExpression).Symbol?.ContainingNamespace.ToDisplayString();

    protected void AddRule(string errorCode, string rule,
        string message, params (string Key, string Value)[] placeholders) =>
        EmittableTypeBuilder.AddRule(EmittableRuleBuilder.From(errorCode, rule, message, placeholders));
}