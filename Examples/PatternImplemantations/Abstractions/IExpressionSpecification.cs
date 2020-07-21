using System;
using System.Linq.Expressions;

namespace Abstractions
{
    public interface IExpressionSpecification<T>
    {
        Expression<Func<T, bool>> ToExpression();
    }
}
