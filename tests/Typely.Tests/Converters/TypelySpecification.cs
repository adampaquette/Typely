using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Tests.Converters;

public class TypelySpecification : ITypelySpecification
{
    public void Create(ITypelyBuilder builder)
    {
        builder.OfString().For("StringSerializationTestsType");
        builder.OfInt().For("IntTypeConverterTestsType");
    }
}