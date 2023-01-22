using Typely.Core.Builders;

namespace Typely.Generators.Typely.Parsing;

internal class InvocationResultParserFactory
{
    public static IInvocationResultParser Create(string defaultNamespace, InvocationResult invocationResult)
    {
        if (invocationResult.MembersAccess.Count == 0)
        {
            throw new InvalidOperationException("MemberAccess cannot be empty");
        }

        var builderType = invocationResult.MembersAccess.First().MemberName;
        var membersAccess = invocationResult.MembersAccess.Skip(1);
        var typelyBuilder = new TypelyBuilder(defaultNamespace);

        switch (builderType)
        {
            case nameof(ITypelyBuilder.OfString):
                return new InvocationResultParserOfString(typelyBuilder, membersAccess);
            case nameof(ITypelyBuilder.OfInt):
                return new InvocationResultParserOfInt(typelyBuilder, membersAccess);
            default: throw new InvalidOperationException($"Unknown builder type: {builderType}");
        }
    }
}

internal interface IInvocationResultParser
{
    IReadOnlyList<EmittableType> Parse();
}

internal class InvocationResultParserOfInt : IInvocationResultParser
{
    private readonly TypelyBuilder _containerBuilder;
    private readonly ITypelyBuilderOfInt _builder;
    private IEnumerable<MemberAccess> _membersAccess;

    public InvocationResultParserOfInt(TypelyBuilder containerBuilder, IEnumerable<MemberAccess> membersAccess)
    {
        _containerBuilder = containerBuilder;
        _builder = containerBuilder.OfInt();
        _membersAccess = membersAccess;
    }

    public IReadOnlyList<EmittableType> Parse()
    {
        foreach (var memberAccess in _membersAccess)
        {
            switch (memberAccess.MemberName)
            {
                case nameof(ITypelyBuilder<string>.For):
                    var typeName = memberAccess.ArgumentListSyntax.Arguments.First().ToString();
                    typeName = typeName.Substring(1, typeName.Length - 2);
                    _builder.For(typeName);
                    break;
                default: throw new NotSupportedException(memberAccess.MemberName);
            }
        }

        return _containerBuilder.GetEmittableTypes();
    }
}

internal class InvocationResultParserOfString : IInvocationResultParser
{
    private readonly TypelyBuilder _containerBuilder;
    private readonly ITypelyBuilderOfString _builder;
    private IEnumerable<MemberAccess> _membersAccess;

    public InvocationResultParserOfString(TypelyBuilder containerBuilder, IEnumerable<MemberAccess> membersAccess)
    {
        _containerBuilder = containerBuilder;
        _builder = containerBuilder.OfString();
        _membersAccess = membersAccess;
    }

    public IReadOnlyList<EmittableType> Parse()
    {
        foreach (var memberAccess in _membersAccess)
        {
            switch (memberAccess.MemberName)
            {
                case nameof(ITypelyBuilder<string>.For):
                    var typeName = memberAccess.ArgumentListSyntax.Arguments.First().ToString();
                    typeName = typeName.Substring(1, typeName.Length - 2);
                    _builder.For(typeName);
                    break;
                case nameof(ITypelyBuilderOfString.Length):
                    var length = int.Parse(memberAccess.ArgumentListSyntax.Arguments.First().ToString());
                    _builder.Length(length);
                    break;
                default: throw new NotSupportedException(memberAccess.MemberName);
            }
        }

        return _containerBuilder.GetEmittableTypes();
    }
}