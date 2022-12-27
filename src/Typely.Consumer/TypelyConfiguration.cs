using Typely.Core;

namespace MyTypes;

public class TypelyConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.For<int>("UserId");
        builder.For<string>("Username");
    }
}