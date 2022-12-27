using Typely.Core;

namespace MyTypes;

public class TypelyConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.For<int>("UserId")
            .Namespace("CustomTypes");

        builder.For<string>("Username");
    }
}