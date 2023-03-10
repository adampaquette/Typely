using System.Text.RegularExpressions;
using Typely.Generators.Typely.Parsing;
using Typely.Generators.Typely.Parsing.String;

namespace Typely.Generators.Tests.Typely.Parsing.String;

[UsesVerify]
public class TypelyBuilderOfStringTests
{
    [Fact] public Task NotEqual() => Builder.NotEqual("10").VerifyRules();

    [Fact] public Task NotEmpty() => Builder.NotEmpty().VerifyRules();

    [Fact] public Task Length() => Builder.Length(1, 10).VerifyRules();

    [Fact] public Task ExactLength() => Builder.Length(10).VerifyRules();

    [Fact] public Task MinLength() => Builder.MinLength(10).VerifyRules();

    [Fact] public Task MaxLength() => Builder.MaxLength(10).VerifyRules();

    [Fact] public Task LessThan() => Builder.LessThan("20").VerifyRules();

    [Fact] public Task LessThanOrEqualTo() => Builder.LessThanOrEqual("20").VerifyRules();

    [Fact] public Task GreaterThan() => Builder.GreaterThan("20").VerifyRules();

    [Fact] public Task GreaterThanOrEqualTo() => Builder.GreaterThanOrEqual("20").VerifyRules();

    [Fact] public Task Must() => Builder.Must((x) => x != "").VerifyRules();

    [Fact] public Task Matches() => Builder.Matches(new Regex("[0-9]*")).VerifyRules();

    [Fact] public void Type_Should_Match() => Assert.Equal("Name", GetSingleEmittableType().TypeName);

    [Fact]
    public void Namespace_Should_Match()
    {
        var expectedNamespace = "My";
        Builder.WithNamespace(expectedNamespace);

        Assert.Equal(expectedNamespace, GetSingleEmittableType().Namespace);
    }

    [Fact]
    public void ConstructTypeKind_ShouldBe_Struct()
    {
        Builder.AsStruct();

        Assert.Equal(ConstructTypeKind.Struct, GetSingleEmittableType().ConstructTypeKind);
    }


    [Fact]
    public void ConstructTypeKind_ShouldBe_Class()
    {
        Builder.AsClass();

        Assert.Equal(ConstructTypeKind.Class, GetSingleEmittableType().ConstructTypeKind);
    }

    [Fact]
    public void Name_Should_Match()
    {
        var expectedName = "Owner identifier";
        Builder.WithName(expectedName);

        Assert.Equal(expectedName, GetSingleEmittableType().Name!.Compile().Invoke());
    }

    [Fact]
    public void NameLambda_Should_Match()
    {
        var expectedName = "Owner identifier";
        Builder.WithName(() => expectedName);

        Assert.Equal(expectedName, GetSingleEmittableType().Name!.Compile().Invoke());
    }

    [Fact]
    public void ErrorCode_Should_Match()
    {
        var expectedErrorCode = "ERR001";
        Builder.NotEmpty().WithErrorCode(expectedErrorCode);

        Assert.Equal(expectedErrorCode, GetSingleEmittableRule().ErrorCode);
    }

    [Fact]
    public void Message_Should_Match()
    {
        var expectedMessage = "Error message";
        Builder.NotEmpty().WithMessage(expectedMessage);

        Assert.Equal(expectedMessage, GetSingleEmittableRule().Message.Compile().Invoke());
    }

    [Fact]
    public void MessageLambda_Should_Match()
    {
        var expectedMessage = "Error message";
        Builder.NotEmpty().WithMessage(() => expectedMessage);

        Assert.Equal(expectedMessage, GetSingleEmittableRule().Message.Compile().Invoke());
    }

    private RuleBuilderOfString Builder { get; } = (RuleBuilderOfString)new TypelyBuilderFixture().Create().OfString().For("Name");

    private EmittableType GetSingleEmittableType() => Assert.Single(Builder.GetEmittableTypes());

    private EmittableRule GetSingleEmittableRule() => Assert.Single(GetSingleEmittableType().Rules);
}