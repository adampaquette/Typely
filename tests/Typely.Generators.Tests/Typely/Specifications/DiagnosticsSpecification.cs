using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Specifications;

internal class DiagnosticsSpecification : ITypelySpecification
{
    public void Create(ITypelyBuilder builder)
    {
        CreateTypeFromUnsupportedCall(builder);
        
        var unsupported = CreateTypeFromUnsupportedCall(builder);
        unsupported.NotEmpty();
        
        builder.OfString().For("ShouldGenerate");
    }

    public IRuleBuilderOfInt CreateTypeFromUnsupportedCall(ITypelyBuilder builder) =>
        builder.OfInt().For("AddressId").GreaterThan(0);
}