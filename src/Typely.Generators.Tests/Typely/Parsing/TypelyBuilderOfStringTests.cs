namespace Typely.Generators.Tests.Typely.Parsing;

[UsesVerify]
public class TypelyBuilderOfStringTests
{
    [Fact]
    public void Type_Should_Match()
    {
        var expectedTypeName = "UserId";

        var builder = new TypelyBuilderFixture().Create();
        builder.String().For(expectedTypeName);

        var emittableTypes = builder.GetEmittableTypes();

        var emittableType = Assert.Single(emittableTypes);
        Assert.Equal(expectedTypeName, emittableType.TypeName);
    }

    [Fact]
    public void Namespace_Should_Match()
    {
        var expectedTypeName = "UserId";
        var expectedNamespace = "My";

        var builder = new TypelyBuilderFixture().Create();

        builder.String()
            .For(expectedTypeName)
            .Namespace(expectedNamespace);

        var emittableTypes = builder.GetEmittableTypes();

        var emittableType = Assert.Single(emittableTypes);
        Assert.Equal(expectedNamespace, emittableType.Namespace);
    }

    [Fact]
    public void ErrorCode_Should_Match()
    {
        var expectedTypeName = "UserId";
        var expectedErrorCode = "ERR001";

        var builder = new TypelyBuilderFixture().Create();

        builder.String()
            .For(expectedTypeName)
            .NotEmpty()
            .WithErrorCode(expectedErrorCode);

        var emittableTypes = builder.GetEmittableTypes();

        var emittableType = Assert.Single(emittableTypes);
        var validation = Assert.Single(emittableType.Rules);
        Assert.Equal(expectedErrorCode, validation.ErrorCode);
    }

    [Fact]
    public void Message_Should_Match()
    {
        var expectedTypeName = "UserId";
        var expectedMessage = "Error message";

        var builder = new TypelyBuilderFixture().Create();

        builder.String()
            .For(expectedTypeName)
            .NotEmpty()
            .WithMessage(expectedMessage);

        var emittableTypes = builder.GetEmittableTypes();

        var emittableType = Assert.Single(emittableTypes);
        var validation = Assert.Single(emittableType.Rules);
        Assert.Equal(expectedMessage, validation.Message.Compile().Invoke());
    }

    [Fact]
    public void MessageLambda_Should_Match()
    {
        var expectedTypeName = "UserId";
        var expectedMessage = "Error message";

        var builder = new TypelyBuilderFixture().Create();

        builder.String()
            .For(expectedTypeName)
            .NotEmpty()
            .WithMessage(() => expectedMessage);

        var emittableTypes = builder.GetEmittableTypes();

        var emittableType = Assert.Single(emittableTypes);
        var validation = Assert.Single(emittableType.Rules);
        Assert.Equal(expectedMessage, validation.Message.Compile().Invoke());
    }

    [Fact]
    public void Name_Should_Match()
    {
        var expectedTypeName = "UserId";
        var expectedName = "Owner identifier";

        var builder = new TypelyBuilderFixture().Create();

        builder.String()
            .For(expectedTypeName)
            .Name(expectedName);

        var emittableTypes = builder.GetEmittableTypes();

        var emittableType = Assert.Single(emittableTypes);
        Assert.Equal(expectedName, emittableType.Name!.Compile().Invoke());
    }

    [Fact]
    public void NameLambda_Should_Match()
    {
        var expectedTypeName = "UserId";
        var expectedName = "Owner identifier";

        var builder = new TypelyBuilderFixture().Create();

        builder.String()
            .For(expectedTypeName)
            .Name(() => expectedName);

        var emittableTypes = builder.GetEmittableTypes();

        var emittableType = Assert.Single(emittableTypes);
        Assert.Equal(expectedName, emittableType.Name!.Compile().Invoke());
    }


    //[Fact]
    //public void ObjectType_ShouldBe_Class()
    //{
    //    var builder = new TypelyBuilderFixture().Create();

    //    builder.For<int>("UserId")
    //        .AsClass();

    //    var emittableTypes = builder.GetEmittableTypes();

    //    var emittableType = Assert.Single(emittableTypes);
    //    Assert.Equal(TypeKind.Class, emittableType.TypeKind);
    //}

    //[Fact]
    //public void ObjectType_ShouldBe_Struct()
    //{
    //    var builder = new TypelyBuilderFixture().Create();

    //    builder.For<int>("UserId")
    //        .AsStruct();

    //    var emittableTypes = builder.GetEmittableTypes();

    //    var emittableType = Assert.Single(emittableTypes);
    //    Assert.Equal(TypeKind.Struct, emittableType.TypeKind);
    //}

    //[Fact]
    //public void ObjectType_ShouldBe_Record()
    //{
    //    var builder = new TypelyBuilderFixture().Create();

    //    builder.For<int>("UserId")
    //        .AsRecord();

    //    var emittableTypes = builder.GetEmittableTypes();

    //    var emittableType = Assert.Single(emittableTypes);
    //    Assert.Equal(TypeKind.Record, emittableType.TypeKind);
    //}
}