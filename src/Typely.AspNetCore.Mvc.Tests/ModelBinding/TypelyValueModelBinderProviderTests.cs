using Typely.AspNetCore.Mvc.ModelBinding;

namespace Typely.AspNetCore.Mvc.Tests.ModelBinding;

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