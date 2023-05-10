using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Parsing;

public class ParenthesizedDeclarationSpecification : ITypelySpecification
{
    public void Create(ITypelyBuilder builder)
    {
        var (a, b) = (1, 2);
    }
}