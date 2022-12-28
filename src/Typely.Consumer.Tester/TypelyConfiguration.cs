using Typely.Core;

namespace MyTypes;

public class TypelyConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.For<int>("UserId")
            .Namespace("aa");

        builder.For<string>("Password").Namespace("supername");
    }
}