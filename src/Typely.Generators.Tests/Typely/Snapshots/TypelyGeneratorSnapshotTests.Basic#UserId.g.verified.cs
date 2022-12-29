//HintName: UserId.g.cs
using Typely.Core;

namespace Typely.Generators.Tests.Typely.Configurations
{
    public struct UserId : IValue<Int32, UserId>
    {
        public Int32 Value {get; set;}
    }
}