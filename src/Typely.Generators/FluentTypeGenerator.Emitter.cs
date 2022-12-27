namespace Typely.Generators;

public partial class TypelyGenerator
{
    internal class Emitter
    {
        public string Emit(Typely config)
        {
            var type = config.UnderlyingType.Name;

            return $$"""
                namespace AA
                {
                    public struct {{config.Name}} 
                    {
                        public {{type}} Value {get; set;}
                    }
                }
                """;
        }
    }
}
