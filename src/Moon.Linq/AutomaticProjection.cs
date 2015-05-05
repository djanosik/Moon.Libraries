using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Moon.Linq
{
    /// <summary>
    /// Automatic projection which can be used to project elements to results with matching
    /// properties (1:1).
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <typeparam name="TResult">The type of the value to project elements to.</typeparam>
    public class AutomaticProjection<TSource, TResult> : Projection<TSource, TResult>
        where TSource : class
        where TResult : class
    {
        private static readonly ConcurrentDictionary<string, Expression> cache = new ConcurrentDictionary<string, Expression>();
        private readonly string cacheKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutomaticProjection{TSource, TResult}" /> class.
        /// </summary>
        public AutomaticProjection()
        {
            cacheKey = GetCacheKey();
        }

        /// <summary>
        /// Returns an expression used to project elements.
        /// </summary>
        public override Expression<Func<TSource, TResult>> GetExpression()
            => GetCachedExpression() ?? BuildExpression();

        private MemberAssignment BuildBinding(Expression parameter, MemberInfo destinationProperty, IEnumerable<PropertyInfo> sourceProperties)
        {
            var sourceProperty = sourceProperties.FirstOrDefault(x => x.Name == destinationProperty.Name);

            if (sourceProperty != null)
            {
                return Expression.Bind(destinationProperty, Expression.Property(parameter, sourceProperty));
            }

            return null;
        }

        private Expression<Func<TSource, TResult>> BuildExpression()
        {
            var sourceProperties = typeof(TSource).GetRuntimeProperties();
            var destinationProperties = typeof(TResult).GetRuntimeProperties().Where(x => x.CanWrite);
            var parameterExpression = Expression.Parameter(typeof(TSource), "src");

            var bindings = destinationProperties
                .Select(destinationProperty => BuildBinding(parameterExpression, destinationProperty, sourceProperties))
                .Where(binding => binding != null);

            var expression = Expression.Lambda<Func<TSource, TResult>>(Expression.MemberInit(Expression.New(typeof(TResult)), bindings), parameterExpression);
            cache.TryAdd(cacheKey, expression);
            return expression;
        }

        private Expression<Func<TSource, TResult>> GetCachedExpression()
            => cache.ContainsKey(cacheKey) ? cache[cacheKey] as Expression<Func<TSource, TResult>> : null;

        private string GetCacheKey()
            => string.Concat(typeof(TSource).FullName, typeof(TResult).FullName);
    }
}