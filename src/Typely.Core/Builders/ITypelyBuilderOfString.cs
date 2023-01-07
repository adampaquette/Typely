namespace Typely.Core.Builders;

public interface ITypelyBuilderOfString : ITypelyBuilder<ITypelyBuilderOfString, IRuleBuilderOfString, string, IFactoryOfString>,
    IComparableRuleBuilder<IRuleBuilderOfString, string>
{
    IRuleBuilderOfString Length(int min, int max);
    IRuleBuilderOfString Length(int exactLength);
    IRuleBuilderOfString MinLength(int minLength);
    IRuleBuilderOfString MaxLength(int maxLength);
    //IRuleBuilderOfString Matches(string regex);
}

public interface IRuleBuilderOfString : IRuleBuilder<ITypelyBuilderOfString, IRuleBuilderOfString, string, IFactoryOfString>
{
}

public interface IFactoryOfString : ITypelyFactory<ITypelyBuilderOfString> 
{
}