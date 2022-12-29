//HintName: Password.g.cs
using Typely.Core;

namespace Typely.Generators.Tests.Typely.Configurations
{
    public struct Password : IValue<String, Password>
    {
        public String Value {get; set;}
    }
}