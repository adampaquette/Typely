//HintName: IsExcluded.g.cs
using Typely.Core;

namespace Typely.Generators.Tests.Typely.Configurations
{
    public struct IsExcluded : IValue<Boolean, IsExcluded>
    {
        public Boolean Value {get; set;}
    }
}