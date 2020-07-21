using System;
using System.Linq.Expressions;

namespace Specifications
{
    public class FilterByGreaterThanOrEqualFieldsSpecification<T> : FilterByFieldsSpecification<T>
    {
        public FilterByGreaterThanOrEqualFieldsSpecification(T objectFilter) : base(objectFilter)
        {

        }

        protected override Func<Expression, Expression, BinaryExpression> BinaryExpression => Expression.GreaterThanOrEqual;
    }
}
