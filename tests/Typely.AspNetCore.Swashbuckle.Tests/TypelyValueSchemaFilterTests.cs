using Microsoft.OpenApi.Models;

namespace Typely.AspNetCore.Swashbuckle.Tests;

public class TypelyValueSchemaFilterTests
{
    private readonly TypelyValueSchemaFilter _filter = new();

    [Fact]
    public void ApplyTheUnderlyingSchemaType_When_ImplementsITypelyValue()
    {
        var schema = new OpenApiSchema();
        var context = new SchemaFilterContextFixture()
            .WithType<ByteTest>()
            .Create();

        _filter.Apply(schema, context);

        Assert.Equal("string", schema.Type);
        Assert.Single(schema.Properties.Values);
    }

    [Fact]
    public void SchemaIsNotTouched_When_TypeDoesNotImplementITypelyValue()
    {
        var schema = new OpenApiSchema();
        var context = new SchemaFilterContextFixture()
            .WithType<int>()
            .Create();

        _filter.Apply(schema, context);

        Assert.Null(schema.Type);
        Assert.Empty(schema.Properties.Values);
    }
}