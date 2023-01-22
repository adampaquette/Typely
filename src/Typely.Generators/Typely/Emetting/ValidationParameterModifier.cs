using System.Linq.Expressions;
using Typely.Core;

namespace Typely.Generators.Typely.Emetting;

/// <summary>
/// Visit a lambda expression and modify the parameter to match the <see cref="ITypelyValue{T}.Value"/> property.
/// </summary>
internal class ValidationParameterModifier : ExpressionVisitor
{
    private ParameterExpression ParameterExp { get; }

    public ValidationParameterModifier(ParameterExpression parameterExp)
    {
        ParameterExp = parameterExp;
    }

    /// <summary>
    /// Visit the lambda expression and modify the parameter to match the <see cref="ITypelyValue{T}.Value"/> property.
    /// </summary>
    /// <param name="expression">Expression to modify.</param>
    /// <returns>The modified expression.</returns>
    public LambdaExpression Modify(LambdaExpression expression) => (LambdaExpression)Visit(expression)!;

    protected override Expression VisitParameter(ParameterExpression node) =>
        node == ParameterExp
            ? Expression.Parameter(node.Type, Consts.ValidateParameterName)
            : base.VisitParameter(node);
}