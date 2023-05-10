using Typely.Core;
using Typely.Core.Builders;

namespace Typely.AspNetCore.Tests.Mvc.ModelBinding;

public class TypelySpecification : ITypelySpecification
{
    public void Create(ITypelyBuilder builder)
    {
        builder.OfString().For("Code").Length(4);
        builder.OfInt().For("Id").GreaterThan(0);
    }
}