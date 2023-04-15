using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Typely.Core;
using Typely.Core.Builders;
using Typely.Generators.Typely.Emitting;

namespace Typely.Generators.Typely.Parsing.TypeBuilders;

internal class EmittableTypeBuilderBase
{
    protected readonly IEnumerable<ParsedInvocation> Invocations;
    protected readonly EmittableType EmittableType;
    private readonly SemanticModel _model;


    protected EmittableTypeBuilderBase(IEnumerable<ParsedInvocation> invocations, EmittableType emittableType,
        SemanticModel model)
    {
        Invocations = invocations;
        EmittableType = emittableType;
        _model = model;
    }

    protected bool TryHandleInvocation(ParsedInvocation invocation) =>
        TryHandleCommonInvocation(invocation) ||
        TryHandleValidationInvocation(invocation) ||
        TryHandleIComparableInvocation(invocation);

    private bool TryHandleCommonInvocation(ParsedInvocation invocation)
    {
        switch (invocation.MemberName)
        {
            case nameof(ITypelyBuilder<int>.For):
                EmittableType.SetTypeName(invocation.GetFirstStringArgument());
                return true;
            case nameof(ITypelyBuilder<int>.AsClass):
                EmittableType.AsClass();
                return true;
            case nameof(ITypelyBuilder<int>.AsStruct):
                EmittableType.AsStruct();
                return true;
            case nameof(ITypelyBuilder<int>.WithName):
                if (invocation.GetFirstExpressionArgument() is ParenthesizedLambdaExpressionSyntax lambdaExpression1)
                {
                    var expNamespace = GetContainingNamespace(lambdaExpression1);
                    EmittableType.AdditionalNamespaces.Add(expNamespace!);
                    EmittableType.SetName(lambdaExpression1.Body.ToString());
                }
                else
                {
                    EmittableType.SetName($"\"{invocation.GetFirstStringArgument()}\"");
                }

                return true;
            case nameof(ITypelyBuilder<int>.WithNamespace):
                EmittableType.SetNamespace(invocation.GetFirstStringArgument());
                return true;
            case nameof(IRuleBuilderOfInt.AsFactory):
                //TODO : Support as factory to reflet code
                return true;
            default: return false;
        }
    }

    private bool TryHandleValidationInvocation(ParsedInvocation invocation)
    {
        switch (invocation.MemberName)
        {
            case nameof(ITypelyBuilderOfInt.NotEmpty):
                AddRule(
                    errorCode: ErrorCodes.NotEmpty,
                    rule: $"{Emitter.ValueParameterName} == default",
                    message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.NotEmpty));
                return true;
            case nameof(ITypelyBuilderOfInt.NotEqual):
                var value = invocation.GetFirstNumberArgument();
                AddRule(
                    errorCode: ErrorCodes.NotEqual,
                    rule: $"{Emitter.ValueParameterName}.Equals({value})",
                    message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.NotEqual),
                    placeholders: (ValidationPlaceholders.ComparisonValue, value));
                return true;
            case nameof(ITypelyBuilderOfInt.Must):
                value = invocation.GetLambdaBodyOfFirstArgument();
                AddRule(
                    errorCode: ErrorCodes.Must,
                    rule: $"!({value})",
                    message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.Must));
                return true;
            case nameof(IRuleBuilderOfInt.WithMessage):
                if (invocation.GetFirstExpressionArgument() is ParenthesizedLambdaExpressionSyntax lambdaExpression2)
                {
                    var expNamespace = GetContainingNamespace(lambdaExpression2);
                    EmittableType.AdditionalNamespaces.Add(expNamespace!);
                    EmittableType.SetCurrentMessage(lambdaExpression2.Body.ToString());
                }
                else
                {
                    EmittableType.SetCurrentMessage($"\"{invocation.GetFirstStringArgument()}\"");
                }

                return true;
            case nameof(IRuleBuilderOfInt.WithErrorCode):
                EmittableType.SetCurrentErrorCode(invocation.GetFirstStringArgument());
                return true;
            default: return false;
        }
    }

    private bool TryHandleIComparableInvocation(ParsedInvocation invocation)
    {
        switch (invocation.MemberName)
        {
            case nameof(ITypelyBuilderOfInt.GreaterThan):
                var value = invocation.GetFirstNumberArgument();
                AddRule(
                    errorCode: ErrorCodes.GreaterThan,
                    rule: $"{Emitter.ValueParameterName} <= {value}",
                    message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.GreaterThan),
                    placeholders: (ValidationPlaceholders.ComparisonValue, value));
                return true;
            case nameof(ITypelyBuilderOfInt.GreaterThanOrEqualTo):
                value = invocation.GetFirstNumberArgument();
                AddRule(
                    errorCode: ErrorCodes.GreaterThanOrEqualTo,
                    rule: $"{Emitter.ValueParameterName} < {value}",
                    message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.GreaterThanOrEqualTo),
                    placeholders: (ValidationPlaceholders.ComparisonValue, value));
                return true;
            case nameof(ITypelyBuilderOfInt.LessThan):
                value = invocation.GetFirstNumberArgument();
                AddRule(
                    errorCode: ErrorCodes.LessThan,
                    rule: $"{Emitter.ValueParameterName} >= {value}",
                    message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.LessThan),
                    placeholders: (ValidationPlaceholders.ComparisonValue, value));
                return true;
            case nameof(ITypelyBuilderOfInt.LessThanOrEqualTo):
                value = invocation.GetFirstNumberArgument();
                AddRule(
                    errorCode: ErrorCodes.LessThanOrEqualTo,
                    rule: $"{Emitter.ValueParameterName} > {value}",
                    message: nameof(ErrorMessages) + "." + nameof(ErrorMessages.LessThanOrEqualTo),
                    placeholders: (ValidationPlaceholders.ComparisonValue, value));
                return true;
            default: return false;
        }
    }

    protected string? GetContainingNamespace(ParenthesizedLambdaExpressionSyntax lambdaExpression) =>
        _model.GetSymbolInfo(lambdaExpression).Symbol?.ContainingNamespace.ToDisplayString();

    protected void AddRule(string errorCode, string rule,
        string message, params (string Key, object Value)[] placeholders) =>
        EmittableType.AddRule(EmittableRule.From(errorCode, rule, message, placeholders));
}