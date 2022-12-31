using System.Linq.Expressions;

namespace Typely.Generators.Typely.Emetting;

internal class ValidationParameterModifier : ExpressionVisitor
{
    private ParameterExpression _parameterExp { get; }
    
    public ValidationParameterModifier(ParameterExpression parameterExp)
    {
        _parameterExp = parameterExp;
    }

    public Expression Modify(Expression expression)
    {
        return Visit(expression);
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        if (node == _parameterExp)
        {
            return Expression.Parameter(node.Type, "Value");
        }
        
        return base.VisitParameter(node);
    }
}
