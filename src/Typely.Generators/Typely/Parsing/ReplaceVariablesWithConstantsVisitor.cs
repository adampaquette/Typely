using System.Linq.Expressions;
using System.Reflection;

namespace Typely.Generators.Typely.Parsing;

/// <summary>
/// Expression visitor used to replace variables by constants.
/// </summary>
internal class ReplaceVariablesWithConstantsVisitor : ExpressionVisitor
{
    protected override Expression VisitMember(MemberExpression node)
    {
        var expression = Visit(node.Expression);
        if (expression is ConstantExpression constantExpression)
        {
            var variable = constantExpression.Value;
            var value = node.Member is FieldInfo info ?
                info.GetValue(variable) :
                ((PropertyInfo)node.Member).GetValue(variable);

            return Expression.Constant(value, node.Type);
        }
        return node.Update(expression);
    }
}