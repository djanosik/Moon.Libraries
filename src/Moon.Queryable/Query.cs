using System.Linq;

namespace Moon.Queryable
{
    /// <summary>
    /// Represents an extensibility point you can use to create reusable queries.
    /// </summary>
    /// <typeparam name="TSource">The type of queried elements.</typeparam>
    public class Query<TSource>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Query{T}" /> class.
        /// </summary>
        /// <param name="source">The source of elements to query.</param>
        public Query(IQueryable<TSource> source)
        {
            Source = source;
        }

        /// <summary>
        /// Gets the source of elements to query.
        /// </summary>
        public IQueryable<TSource> Source { get; private set; }
    }
}