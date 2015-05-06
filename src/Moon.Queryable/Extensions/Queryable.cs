using System.Linq;

namespace Moon.Queryable
{
    /// <summary>
    /// <see cref="IQueryable{T}" /> extension methods.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Allows to project each element of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">A sequence of values to project.</param>
        public static ProjectionExpression<TSource> Project<TSource>(this IQueryable<TSource> source)
            where TSource : class
            => new ProjectionExpression<TSource>(source);

        /// <summary>
        /// Filters a sequence of values based on a query.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IQueryable{T}" /> to filter.</param>
        public static Query<TSource> Where<TSource>(this IQueryable<TSource> source)
            => new Query<TSource>(source);
    }
}