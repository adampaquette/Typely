using Typely.Generators.Typely.Parsing;
using Typely.Generators.Typely.Parsing.TypeBuilders;

namespace Typely.Generators.Tests.Typely.Parsing;

public class EmittableTypeBuilderFactoryTests
{
    [Fact]
    public void UnsupportedType_Should_Throw()
    {
        var statement = new ParsedStatement(null!) { Root = "builder", };
        statement.Invocations.Add(new ParsedInvocation(null!, "OfUnsupportedType"));

        Assert.Throws<NotSupportedException>(() => EmittableTypeBuilderFactory.Create("namespace", statement));
    }
}