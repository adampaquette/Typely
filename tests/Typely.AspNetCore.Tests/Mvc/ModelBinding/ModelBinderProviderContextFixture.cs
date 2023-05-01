using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Moq;

namespace Typely.AspNetCore.Tests.Mvc.ModelBinding;

public class ModelBinderProviderContextFixture
{
    private Type? _modelType;

    public ModelBinderProviderContextFixture WithModelType(Type modelType)
    {
        _modelType = modelType;
        return this;
    }

    public TestModelBinderProviderContext Build()
    {
        var metadetailsProvider = new Mock<ICompositeMetadataDetailsProvider>();
        var modelMetadataProvider = new Mock<IModelMetadataProvider>();
        var modelMetadata =
            new DefaultModelMetadataProvider(metadetailsProvider.Object)
                .GetMetadataForType(_modelType!);
        return new TestModelBinderProviderContext(modelMetadata, modelMetadataProvider.Object);
    }
}