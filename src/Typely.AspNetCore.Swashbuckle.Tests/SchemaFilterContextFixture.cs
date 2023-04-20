using AutoFixture;
using Microsoft.OpenApi.Models;
using Moq;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Typely.AspNetCore.Swashbuckle.Tests;

public class SchemaFilterContextFixture : BaseFixture<SchemaFilterContext>
{
    public SchemaFilterContextFixture()
    {
        Fixture.Register<MemberInfo?>(() => null);
        Fixture.Register<ParameterInfo?>(() => null);
    }

    public SchemaFilterContextFixture WithType<T>()
    {
        Fixture.Register(() => typeof(T));
        Fixture.Freeze<Mock<ISchemaGenerator>>()
            .Setup(x =>
                x.GenerateSchema(It.IsAny<Type>(), It.IsAny<SchemaRepository>(), null, null, null))
            .Returns(new OpenApiSchema
            {
                Type = "string", 
                Properties = new Dictionary<string, OpenApiSchema>
                {
                    ["property1"] = new() { Type = "string" }
                }
            });
        return this;
    }
}