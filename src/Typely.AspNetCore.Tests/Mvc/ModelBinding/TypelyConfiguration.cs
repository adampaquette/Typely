using Typely.Core;
using Typely.Core.Builders;

namespace Typely.AspNetCore.Tests.Mvc.ModelBinding;

public class TypelyConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfString().For("Code").Length(4);
    }
}