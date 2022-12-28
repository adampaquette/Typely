using Typely.Core;

namespace MyTypes;

public class TypelyConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.For<int>("UserId")
            .Namespace("aa")
            .AsClass();

        builder.For<string>("Password")
            .Namespace("supername")
            .AsRecord();

        builder.For<string>("Password2")
            .AsStruct();
    }
}