//HintName: Key.g.cs
using Typely.Core;

namespace C
{
    public struct Key : IValue<Guid, Key>
    {
        public Guid Value {get; set;}
    }
}