using AutoFixture;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Typely.Core;

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
        Fixture.Register(() =>
        {
            var repository = new SchemaRepository();
            repository.RegisterType(GetTypelyValueUnderlyingType(typeof(T)), "id");
            return repository;
        });
        return this;
    }
    
    private static Type? GetTypelyValueUnderlyingType(Type type) =>
        type.GetInterfaces()
            .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ITypelyValue<>))
            .GenericTypeArguments[0];
}