using Typely.Core;
using Typely.Core.Builders;

namespace Typely.EfCore.Tests;

public class TypelySpecification : ITypelySpecification
{
    public void Create(ITypelyBuilder builder)
    {
        builder.OfString().For("MyString");
    }
}

public class TypelyValueConverterTests
{
    private readonly TypelyValueConverter<string, MyString> converter = new();
    const string expectedString = "MyValue";
    readonly MyString expected = MyString.From(expectedString);

    [Fact]
    public void ConvertFromProvider() =>
        Assert.Equal(expected, converter.ConvertFromProvider(expectedString));
    
    [Fact]
    public void ConvertToProvider() =>
        Assert.Equal(expectedString, converter.ConvertToProvider(expected));
}