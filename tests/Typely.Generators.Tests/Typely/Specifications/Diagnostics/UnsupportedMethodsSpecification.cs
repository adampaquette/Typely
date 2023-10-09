using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Specifications.Diagnostics;

public class UnsupportedMethodsSpecification: ITypelySpecification
{
    public void Create(ITypelyBuilder builder)
    {
        builder.OfString().For("Unsupported").UnsupportedExtension();

        CreateTypeFromUnsupportedCall(builder);

        var unsupported = CreateTypeFromUnsupportedCall(builder);
        unsupported.NotEmpty();
    }

    public IRuleBuilderOfInt CreateTypeFromUnsupportedCall(ITypelyBuilder builder) =>
        builder.OfInt().For("AddressId").GreaterThan(0);
}

internal static class TypelyBuilderExtensions
{
    public static void UnsupportedExtension(this ITypelyBuilderOfString builder) =>
        builder.MinLength(3).MaxLength(20).Normalize(x => x.ToUpper());
}