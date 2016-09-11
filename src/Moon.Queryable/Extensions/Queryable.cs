using System.Linq;

namespace Moon.Queryable
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Filters a sequence of values based on a query.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IQueryable{T}" /> to filter.</param>
        public static Query<TSource> Where<TSource>(this IQueryable<TSource> source)
            => new Query<TSource>(source);
    }
}