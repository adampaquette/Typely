using Typely.Generators.Typely.Parsing.String;
using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Tests.Typely.Parsing.String;

[UsesVerify]
public class FactoryOfStringTests
{
    [Fact]
    public void Type_Should_Match()
    {
        var expectedName = "Name";
        Factory.For(expectedName);

        Assert.Equal(expectedName, GetSingleEmittableType().TypeName);
    }

    [Fact]
    public void Namespace_Should_Match()
    {
        var expectedNamespace = "My";
        Factory.WithNamespace(expectedNamespace);

        Assert.Equal(expectedNamespace, GetSingleEmittableType().Namespace);
    }

    [Fact]
    public void ConstructTypeKind_ShouldBe_Struct()
    {
        Factory.AsStruct();

        Assert.Equal(ConstructTypeKind.Struct, GetSingleEmittableType().ConstructTypeKind);
    }

    [Fact]
    public void Name_Should_Match()
    {
        var expectedName = "Owner identifier";
        Factory.WithName(expectedName);

        Assert.Equal(expectedName, GetSingleEmittableType().Name!.Compile().Invoke());
    }

    [Fact]
    public void NameLambda_Should_Match()
    {
        var expectedName = "Owner identifier";
        Factory.WithName(() => expectedName);

        Assert.Equal(expectedName, GetSingleEmittableType().Name!.Compile().Invoke());
    }

    private FactoryOfString Factory { get; } = (FactoryOfString)new TypelyBuilderFixture().Create().OfString().AsFactory();

    private EmittableType GetSingleEmittableType() => Assert.Single(Factory.GetEmittableTypes());
}