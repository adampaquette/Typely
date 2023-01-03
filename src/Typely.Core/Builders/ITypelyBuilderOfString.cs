using System;
using System.Collections.Generic;
using System.Text;

namespace Typely.Core.Builders;

public interface ITypelyBuilderOfString : ITypelyBuilder<int, IRuleBuilderOfString, ITypelyBuilderOfString>
{
    ITypelyBuilderOfString Length(int min, int max); //string
    ITypelyBuilderOfString Length(int exactLength); //string
    ITypelyBuilderOfString MinLength(int minLength); //string
    ITypelyBuilderOfString MaxLength(int maxLength); //string
    //ITypelyBuilderOfString Matches(string regex); //string
    ITypelyBuilderOfString LessThan(string value); //IComparable
    ITypelyBuilderOfString LessThanOrEqual(string value); //IComparable
    ITypelyBuilderOfString GreaterThan(string value); //IComparable
    ITypelyBuilderOfString GreaterThanOrEqual(string value); //IComparable
}

public interface IRuleBuilderOfString :
    IRuleBuilder<int, IRuleBuilderOfString>,
    ITypelyBuilder<int, IRuleBuilderOfString, ITypelyBuilderOfString>
{
}