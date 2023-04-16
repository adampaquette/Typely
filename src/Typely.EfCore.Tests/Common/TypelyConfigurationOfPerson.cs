using System.Text.RegularExpressions;
using Typely.Core;
using Typely.Core.Builders;

namespace Typely.EfCore.Tests.Common;

public class TypelyConfigurationOfPerson : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.OfInt().For("PersonId").NotEmpty();
        builder.OfString().For("FirstName").NotEmpty();
        builder.OfString().For("Email").NotEmpty().MaxLength(100)
            .Matches(new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"));
    }
}