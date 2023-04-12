using Typely.Core.Builders;

namespace Typely.Generators.Typely.Parsing.TypeBuilders;

internal class EmittableTypeBuilderBase
{
    protected readonly IEnumerable<ParsedInvocation> Invocations;
    protected readonly EmittableType EmittableType;

    protected EmittableTypeBuilderBase(IEnumerable<ParsedInvocation> invocations, EmittableType emittableType)
    {
        Invocations = invocations;
        EmittableType = emittableType;
    }

    protected bool TryHandleInvocation(ParsedInvocation invocation)
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
                //TODO : IS lambda expression
                //else
                EmittableType.SetName($"\"{invocation.GetFirstStringArgument()}\"");
                return true;
            case nameof(ITypelyBuilder<int>.WithNamespace):
                EmittableType.SetNamespace(invocation.GetFirstStringArgument());
                return true;
            case nameof(IRuleBuilderOfInt.WithMessage):
                EmittableType.SetCurrentMessage($"\"{invocation.GetFirstStringArgument()}\"");
                return true;
            case nameof(IRuleBuilderOfInt.WithErrorCode):
                EmittableType.SetCurrentErrorCode(invocation.GetFirstStringArgument());
                return true;
            case nameof(IRuleBuilderOfInt.AsFactory):
                //TODO : Support as factory to reflet code
                return true;
        }

        return false;
    }

    protected void AddRule(string errorCode, string rule,
        string message, params (string Key, object Value)[] placeholders) =>
        EmittableType.AddRule(EmittableRule.From(errorCode, rule, message, placeholders));
}