using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Parsing;

public class ParenthesizedDeclarationConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        var (a, b) = (1, 2);
    }
}