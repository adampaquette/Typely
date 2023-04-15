using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Tests.Converters;

public class TypelyConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfString().For("StringSerializationTestsType");
    }
}