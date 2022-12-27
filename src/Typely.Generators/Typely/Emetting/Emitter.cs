using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Typely.Emetting;

internal class Emitter
{
    public string Emit(EmittableType emittableType)
    {
        var type = emittableType.UnderlyingType!.Name;

        return $$"""
                namespace {{emittableType.Namespace}}
                {
                    public struct {{emittableType.Name}} 
                    {
                        public {{type}} Value {get; set;}
                    }
                }
                """;
    }
}