using Newtonsoft.Json.Linq;
using System.Resources;
using Typely.Core;

namespace Typely.Tests;

public class ReferenceSample : ValueBase<int, ReferenceSample>
{
    private ReferenceSample() { }
    public ReferenceSample(int value) : base(value) { }    
    public static ReferenceSample From(int value) => new(value);
}

public class NotEmptyValidationEmitter
{
    public string Emit<T>(T type, ResourceManager resourceManager)
    {
        return ErrorMessages.NotEmpty;
    }
}