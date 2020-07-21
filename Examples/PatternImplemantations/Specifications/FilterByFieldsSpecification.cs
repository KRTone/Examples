using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Specifications
{
    public class FilterByFieldsSpecification<T> : CompositeSpecification<T>
    {
        readonly T objectFilter;
        protected virtual Func<Expression, Expression, BinaryExpression> BinaryExpression => Expression.Equal;

        public FilterByFieldsSpecification(T objectFilter)
        {
            this.objectFilter = objectFilter;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            List<PropValue> propsAndValues = GetFilterProperties(objectFilter);

            Expression expression = null;
            var parameter = Expression.Parameter(typeof(T), "r");
            foreach (var propValue in propsAndValues)
            {
                PropertyInfo prop = propValue.Property;
                if (objectFilter.GetType().GetProperties().Contains(prop) && (prop.PropertyType.IsValueType || prop.PropertyType == typeof(string)))
                {
                    var property = Expression.Property(parameter, prop.Name);
                    var constant = Expression.Constant(prop.GetValue(objectFilter), prop.PropertyType);
                    var equal = BinaryExpression(property, constant);
                    expression = expression == null ? equal : Expression.AndAlso(expression, equal);
                }
                else
                {
                    var fullName = GetFullPropertyName(typeof(T), prop).First();
                    var equal = BuildNestedObjectsExpression(fullName, propValue.Value, parameter);
                    expression = expression == null ? equal : Expression.AndAlso(expression, equal);
                }
            }

            return expression != null ? Expression.Lambda<Func<T, bool>>(expression, parameter) : r => true;
        }

        List<PropValue> GetFilterProperties(object obj)
        {
            List<PropValue> props = new List<PropValue>();
            foreach (PropertyInfo prop in obj.GetType().GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                Type propType = prop.PropertyType;
                object propValue = prop.GetValue(obj);
                // Если значение не равно значению по умолчанию, запоминаем свойство.
                if ((propValue as ICollection == null)
                    && ((propType.IsValueType
                            && propValue is IComparable
                            && ((propValue as IComparable).CompareTo(Activator.CreateInstance(propType)) != 0))
                        || (!propType.IsValueType && propValue != null))
                    && prop.CanRead
                    && prop.CanWrite)
                {
                    if (propType.IsValueType || propType == typeof(string))
                        props.Add(new PropValue(prop, propValue));
                    else
                        props.AddRange(GetFilterProperties(propValue));
                }
            }
            return props;
        }

        IEnumerable<string> GetFullPropertyName(Type baseType, PropertyInfo prop)
        {
            var curProp = baseType.GetProperty(prop.Name);
            if (curProp != null && curProp == prop)
            {
                return new[] { curProp.Name };
            }
            //предотвращаем бесконечную рекурсию из-за классов имеющих рекурсивную вложенность
            else if (baseType.IsClass && !baseType.GetProperties().Select(s => s.PropertyType).Contains(baseType))
            {
                return baseType
                    .GetProperties()
                    .SelectMany(p => GetFullPropertyName(p.PropertyType, prop), (p, v) => p.Name + "." + v);
            }
            return Enumerable.Empty<string>();
        }

        Expression BuildNestedObjectsExpression(string member, object value, ParameterExpression parameter)
        {
            Expression body = parameter;
            foreach (var subMember in member.Split('.'))
            {
                body = Expression.PropertyOrField(body, subMember);
            }
            return BinaryExpression(body, Expression.Constant(value, body.Type));
        }

        class PropValue
        {
            public PropValue(PropertyInfo prop, object value)
            {
                Property = prop;
                Value = value;
            }

            public PropertyInfo Property { get; }
            public object Value { get; }
        }
    }
}
