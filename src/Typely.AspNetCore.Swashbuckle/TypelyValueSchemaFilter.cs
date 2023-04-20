using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Typely.Core;

namespace Typely.AspNetCore.Swashbuckle;

public class TypelyValueSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var typelyValueType = GetTypelyValueTypeOrDefault(context.Type);
        if (typelyValueType is null)
        {
            return;
        }
        
        var valueType = typelyValueType.GenericTypeArguments[0];
        if (!context.SchemaRepository.TryLookupByType(valueType, out var valueSchema))
        {
            valueSchema = context.SchemaGenerator.GenerateSchema(valueType, context.SchemaRepository);
        }

        schema.Type = valueSchema.Type;
        foreach (var (key, prop) in valueSchema.Properties)
        {
            schema.Properties.Add(key, prop);
        }
    }

    private static Type? GetTypelyValueTypeOrDefault(Type type) => 
        type.GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ITypelyValue<>));
}