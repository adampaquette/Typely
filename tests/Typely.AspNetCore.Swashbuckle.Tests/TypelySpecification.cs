using Typely.Core;
using Typely.Core.Builders;

namespace Typely.AspNetCore.Swashbuckle.Tests;

public class TypelySpecification : ITypelySpecification
{
    public void Create(ITypelyBuilder builder)
    {
        builder.OfByte().For("ByteTest");
    }
}