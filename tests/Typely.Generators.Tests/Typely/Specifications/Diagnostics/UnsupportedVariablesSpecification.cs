using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Specifications.Diagnostics;

public class UnsupportedVariablesSpecification : ITypelySpecification
{
    public void Create(ITypelyBuilder builder)
    {
        var factory = builder.OfString().AsFactory();
        factory.For("BasicType");

        const string constVar1 = "constVar1";
        string var2 = "var2";
        int i = 0;

        builder.OfString().For(constVar1);
        builder.OfString().For(var2);
        builder.OfString().For(i.ToString());

        var number = builder.OfInt().LessThan(100);
        number.For("A");
    }
}