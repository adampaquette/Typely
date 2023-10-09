using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Specifications.Diagnostics;

public class UnsupportedPropertiesSpecification : ITypelySpecification
{
    private string TypeName => "MyType";

    public void Create(ITypelyBuilder builder)
    {
        builder.OfString().For(TypeName);
    }
}
