using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Typely.Emetting;

internal class Emitter
{
    public string Emit(EmittableType t)
    {
        var underlyingType = t.UnderlyingType!.Name;
        var objectType = GetObjectType(t.TypeKind);

        return $$"""
                using Typely.Core;

                namespace {{t.Namespace}}
                {
                    public partial {{objectType}} {{t.Name}} : IValue<{{underlyingType}}, {{t.Name}}>
                    {
                        public {{underlyingType}} Value {get; set;}
                    }
                }
                """;
    }

    public string GetObjectType(TypeKind objectType) => objectType switch
    {
        TypeKind.Struct => "struct",
        TypeKind.Record => "record",
        TypeKind.Class => "class",
        _ => throw new ArgumentOutOfRangeException(nameof(objectType), $"Unexpected value {objectType}"),
    };
}