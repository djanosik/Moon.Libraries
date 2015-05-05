using System;
using System.Linq;
using System.Linq.Expressions;

namespace Moon.Linq
{
    /// <summary>
    /// The base class for element projections.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <typeparam name="TResult">The type of the value to project elements to.</typeparam>
    public abstract class Projection<TSource, TResult>
        where TSource : class
        where TResult : class
    {
        /// <summary>
        /// Gets the sequence of values to project.
        /// </summary>
        public IQueryable<TSource> Source { get; internal set; }

        /// <summary>
        /// Returns an expression used to project elements.
        /// </summary>
        public abstract Expression<Func<TSource, TResult>> GetExpression();
    }
}