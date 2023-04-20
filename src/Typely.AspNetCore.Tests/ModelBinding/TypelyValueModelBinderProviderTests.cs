using Typely.AspNetCore.ModelBinding;

namespace Typely.AspNetCore.Tests.ModelBinding;

public class TypelyValueModelBinderProviderTests
{
    [Fact]
    public void GetBinder_ReturnsModelBinder_WhenModelTypeIsTypelyValue()
    {
        var binderProviderContext = new ModelBinderProviderContextFixture()
            .WithModelType(typeof(Code))
            .Build();

        var binder = new TypelyValueModelBinderProvider().GetBinder(binderProviderContext);
        
        Assert.NotNull(binder);
    }

    [Fact]
    public void GetBinder_ReturnsNull_WhenModelTypeIsNotTypelyValue()
    {
        var binderProviderContext = new ModelBinderProviderContextFixture()
            .WithModelType(typeof(string))
            .Build();

        var binder = new TypelyValueModelBinderProvider().GetBinder(binderProviderContext);

        Assert.Null(binder);
    }
}