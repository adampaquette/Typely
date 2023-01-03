using System;
using System.Collections.Generic;
using System.Text;
using Typely.Core.Builders;

namespace Typely.Generators.Typely.Parsing.Builders;
internal class RuleBuilderOfString : IRuleBuilderOfString
{
    public IRuleBuilderOfString For(string typeName)
    {
        throw new NotImplementedException();
    }

    public IRuleBuilderOfString NotEmpty()
    {
        throw new NotImplementedException();
    }

    public IRuleBuilder<int, IRuleBuilderOfString> WithErrorCode(string errorCode)
    {
        throw new NotImplementedException();
    }

    ITypelyBuilderOfString ITypelyBuilder<int, IRuleBuilderOfString, ITypelyBuilderOfString>.For(string typeName)
    {
        throw new NotImplementedException();
    }
}
