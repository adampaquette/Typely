using System.Linq.Expressions;

namespace Typely.Generators.Typely.Parsing;

/// <summary>
/// Extensions of Expression.
/// </summary>
internal static class ExpressionExtensions
{
    /// <summary>
    /// Replace all variables used inside the expression with constants.
    /// </summary>
    /// <typeparam name="TDelegate">Type of the delegate.</typeparam>
    /// <param name="source">Expression tree to modify.</param>
    /// <returns>The same expression tree with constants instead of variables.</returns>
    public static Expression<TDelegate> ReplaceVariablesWithConstants<TDelegate>(this Expression<TDelegate> source) =>
        source.Update(new ReplaceVariablesWithConstantsVisitor().Visit(source.Body), source.Parameters);

    /// <summary>
    /// Creates a negated expression of the delegate.
    /// </summary>
    /// <typeparam name="TDelegate">Type of the delegate.</typeparam>
    /// <param name="expression">Expression to negate.</param>
    /// <returns>A new expression negated.</returns>
    public static Expression<TDelegate> Negate<TDelegate>(this Expression<TDelegate> expression) =>
        Expression.Lambda<TDelegate>(Expression.Not(expression.Body), expression.Parameters);    
}