using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Typely.AspNetCore.Tests.ModelBinding;

public class ModelBinderProviderContextFixture
{
    private Type? _modelType = null;

    public ModelBinderProviderContextFixture WithModelType(Type modelType)
    {
        _modelType = modelType;
        return this;
    }

    public TestModelBinderProviderContext Build()
    {
        var modelMetadata =
            new DefaultModelMetadataProvider(
                    new DefaultCompositeMetadataDetailsProvider(Array.Empty<IMetadataDetailsProvider>()))
                .GetMetadataForType(_modelType);
        return new TestModelBinderProviderContext(modelMetadata);
    }
}