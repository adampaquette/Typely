using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Typely.Emetting;

internal class Emitter
{
    public string Emit(EmittableType t)
    {
        var underlyingType = t.UnderlyingType!.Name;
        var objectType = GetObjectType(t.ObjectType);

        return $$"""
                namespace {{t.Namespace}}
                {
                    public {{objectType}} {{t.Name}} 
                    {
                        public {{underlyingType}} Value {get; set;}
                    }
                }
                """;
    }

    public string GetObjectType(ObjectType objectType) => objectType switch
    {
        ObjectType.Struct => "struct",
        ObjectType.Record => "record",
        ObjectType.Class => "class",
        _ => throw new ArgumentOutOfRangeException(nameof(objectType), $"Unexpected value {objectType}"),
    };
}