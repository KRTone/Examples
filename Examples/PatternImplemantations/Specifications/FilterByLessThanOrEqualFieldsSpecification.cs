using System;
using System.Linq.Expressions;

namespace Specifications
{
    public class FilterByLessThanOrEqualFieldsSpecification<T> : FilterByFieldsSpecification<T>
    {
        public FilterByLessThanOrEqualFieldsSpecification(T objectFilter) : base(objectFilter)
        {

        }

        protected override Func<Expression, Expression, BinaryExpression> BinaryExpression => Expression.LessThanOrEqual;
    }
}
