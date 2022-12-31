using System.Linq.Expressions;

namespace Typely.Generators.Typely.Parsing;
internal static class ExpressionExtensions
{
    public static Expression<TDelegate> ReplaceVariablesWithConstants<TDelegate>(this Expression<TDelegate> source) =>
        source.Update(new ReplaceVariablesWithConstantsVisitor().Visit(source.Body), source.Parameters);    
}
