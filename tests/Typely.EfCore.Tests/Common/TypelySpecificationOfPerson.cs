using System.Text.RegularExpressions;
using Typely.Core;
using Typely.Core.Builders;

namespace Typely.EfCore.Tests.Common;

public class TypelySpecificationOfPerson : ITypelySpecification
{
    public void Create(ITypelyBuilder builder)
    {
        builder.OfInt().For("PersonId").NotEmpty();
        builder.OfString().For("FirstName").NotEmpty();
        builder.OfString().For("Email").NotEmpty().MaxLength(100)
            .Matches(new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"));
    }
}