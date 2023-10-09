using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Specifications.Diagnostics;

public class UnsupportedTypesSpecification : ITypelySpecification
{
    public class A
    {
    }

    public record B
    {
    }

    private struct C
    {
    }

    protected enum D { }

    public void Create(ITypelyBuilder builder)
    {
    }
}