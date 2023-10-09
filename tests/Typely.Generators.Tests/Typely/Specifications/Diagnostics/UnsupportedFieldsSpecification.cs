using Typely.Core;
using Typely.Core.Builders;

namespace Typely.Generators.Tests.Typely.Specifications.Diagnostics;

public class UnsupportedFieldsSpecification: ITypelySpecification
{
    private const string Field1 = "Field1";
    private string Field2 = "Field2";
    
    public void Create(ITypelyBuilder builder)
    {
        builder.OfString().For(Field1);
        builder.OfString().For(Field2);
    }
}