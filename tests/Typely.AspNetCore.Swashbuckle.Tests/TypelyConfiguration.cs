using Typely.Core;
using Typely.Core.Builders;

namespace Typely.AspNetCore.Swashbuckle.Tests;

public class TypelyConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfByte().For("ByteTest");
    }
}