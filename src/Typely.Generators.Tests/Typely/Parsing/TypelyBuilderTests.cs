using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Tests.Typely.Parsing;

public class TypelyBuilderTests
{
    [Fact]
    public void Type_Should_Match()
    {
        var expectedTypeName = "UserId";

        var builder = new TypelyBuilderFixture().Create();
        builder.For<int>(expectedTypeName);

        var emittableTypes = builder.GetEmittableTypes();

        var emittableType = Assert.Single(emittableTypes);
        Assert.Equal(expectedTypeName, emittableType.Name);
    }

    [Fact]
    public void Namespace_Should_Match()
    {
        var expectedTypeName = "UserId";
        var expectedNamespace = "My";

        var builder = new TypelyBuilderFixture().Create();

        builder.For<int>(expectedTypeName)
            .Namespace(expectedNamespace);

        var emittableTypes = builder.GetEmittableTypes();

        var emittableType = Assert.Single(emittableTypes);
        Assert.Equal(expectedNamespace, emittableType.Namespace);
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