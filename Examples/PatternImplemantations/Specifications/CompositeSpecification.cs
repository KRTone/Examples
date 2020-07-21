using Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Specifications
{
    public abstract class CompositeSpecification<T> : ExpressionSpecification<T>
    {
        public CompositeSpecification<T> And(IExpressionSpecification<T> other) => new AndSpecification(this, other);
        public CompositeSpecification<T> Or(IExpressionSpecification<T> other) => new OrSpecification(this, other);

        abstract class FolderSpecification : CompositeSpecification<T>
        {
            protected readonly IExpressionSpecification<T> left;
            protected readonly IExpressionSpecification<T> right;

            public FolderSpecification(IExpressionSpecification<T> left, IExpressionSpecification<T> right)
            {
                this.left = left ?? throw new ArgumentNullException(nameof(left));
                this.right = right ?? throw new ArgumentNullException(nameof(right));
            }
        }

        class AndSpecification : FolderSpecification
        {
            public AndSpecification(IExpressionSpecification<T> left, IExpressionSpecification<T> right)
                : base(left, right)
            {

            }

            public override Expression<Func<T, bool>> ToExpression()
            {
                var rightExpr = right.ToExpression();
                var leftExpr = left.ToExpression();
                var invokedExpr = Expression.Invoke(rightExpr, leftExpr.Parameters.Cast<Expression>());
                return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(leftExpr.Body, invokedExpr), leftExpr.Parameters);
            }
        }

        class OrSpecification : FolderSpecification
        {
            public OrSpecification(IExpressionSpecification<T> left, IExpressionSpecification<T> right)
                : base(left, right)
            {

            }

            public override Expression<Func<T, bool>> ToExpression()
            {
                var rightExpr = right.ToExpression();
                var leftExpr = left.ToExpression();
                var invokedExpr = Expression.Invoke(rightExpr, leftExpr.Parameters.Cast<Expression>());
                return Expression.Lambda<Func<T, bool>>(Expression.OrElse(leftExpr.Body, invokedExpr), leftExpr.Parameters);
            }
        }
    }
}
