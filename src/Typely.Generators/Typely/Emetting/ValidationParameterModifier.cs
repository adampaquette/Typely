using System.Linq.Expressions;
using Typely.Core;

namespace Typely.Generators.Typely.Emetting;

/// <summary>
/// Visit a lambda expression and modify the parameter to match the <see cref="ITypelyValue{T}.Value"/> property.
/// </summary>
internal class ValidationParameterModifier : ExpressionVisitor
{
    private ParameterExpression _parameterExp { get; }
    
    public ValidationParameterModifier(ParameterExpression parameterExp)
    {
        _parameterExp = parameterExp;
    }

    /// <summary>
    /// Visit the lambda expression and modify the parameter to match the <see cref="ITypelyValue{T}.Value"/> property.
    /// </summary>
    /// <param name="expression">Expression to modify.</param>
    /// <returns>The modified expression.</returns>
    public Expression Modify(LambdaExpression expression)
    {
        return Visit(expression);
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        if (node == _parameterExp)
        {
            return Expression.Parameter(node.Type, Consts.ValidateParameterName);
        }
        
        return base.VisitParameter(node);
    }
}
