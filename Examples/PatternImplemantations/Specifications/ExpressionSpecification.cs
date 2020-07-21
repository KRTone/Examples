using Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Specifications
{
    public abstract class ExpressionSpecification<T> : IExpressionSpecification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();
    }
}
