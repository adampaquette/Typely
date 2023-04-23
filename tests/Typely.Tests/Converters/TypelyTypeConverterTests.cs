using Typely.Core.Converters;

namespace Typely.Tests.Converters;

public class TypelyTypeConverterTests
{
    [Fact]
    public void CanConvertFromInt_ReturnsTrue_WhenUnderlyingTypeIsInt()
    {
        var converter = new TypelyTypeConverter<int, IntTypeConverterTestsType>();
        Assert.True(converter.CanConvertFrom(null, typeof(int)));
    }
    
    [Fact]
    public void ConvertFromInt_ReturnsTypelyType_WhenUnderlyingTypeIsInt()
    {
        var converter = new TypelyTypeConverter<int, IntTypeConverterTestsType>();
        var result = converter.ConvertFrom(null, null, 1);
        Assert.IsType<IntTypeConverterTestsType>(result);
    }
    
    [Fact]
    public void ConvertFromInt_ReturnsTypelyType_WhenUnderlyingTypeIsString()
    {
        var converter = new TypelyTypeConverter<int, IntTypeConverterTestsType>();
        var result = converter.ConvertFrom(null, null, "1");
        Assert.IsType<IntTypeConverterTestsType>(result);
    }
    
    [Fact] 
    public void CanConvertToInt_ReturnsTrue_WhenUnderlyingTypeIsInt()
    {
        var converter = new TypelyTypeConverter<int, IntTypeConverterTestsType>();
        Assert.True(converter.CanConvertTo(null, typeof(int)));
    } 
    
    [Fact]
    public void ConvertToInt_ReturnsTrue_WhenTypeIsTypelyTypeOfInt()
    {
        var converter = new TypelyTypeConverter<int, IntTypeConverterTestsType>();
        var value = IntTypeConverterTestsType.From(1);
        var result = converter.ConvertTo(null, null, value, typeof(int));
        Assert.IsType<int>(result);
    }
}