namespace FluentType.Generators;

public partial class FluentTypeGenerator
{
    internal class Emitter
    {
        public string Emit(FluentType config)
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
