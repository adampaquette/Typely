using Typely.Core;
using Typely.Core.Builders;

namespace Typely.AspNetCore.Mvc.Tests.ModelBinding;

public class TypelyConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfString().For("Code").Length(4);
    }
}