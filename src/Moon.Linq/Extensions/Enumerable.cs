using System.Collections.Generic;
using System.Linq;

namespace Moon.Linq
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Allows to project each element of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">A sequence of values to project.</param>
        public static ProjectionExpression<TSource> Project<TSource>(this IEnumerable<TSource> source)
            where TSource : class
            => new ProjectionExpression<TSource>(source.AsQueryable());


        /// <summary>
        /// Filters a sequence of values based on a query.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}" /> to filter.</param>
        public static Query<TSource> Where<TSource>(this IEnumerable<TSource> source)
            => new Query<TSource>(source.AsQueryable());

    }
}
