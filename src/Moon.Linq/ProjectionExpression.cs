using System.Linq;

namespace Moon.Linq
{
    /// <summary>
    /// The projection expression used to project elements of source into a new form.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    public class ProjectionExpression<TSource>
        where TSource : class
    {
        private readonly IQueryable<TSource> source;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectionExpression{TSource}" /> class.
        /// </summary>
        /// <param name="source">A sequence of values to project.</param>
        public ProjectionExpression(IQueryable<TSource> source)
        {
            this.source = source;
        }

        /// <summary>
        /// Allows to project each element of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by the function.</typeparam>
        public IQueryable<TResult> To<TResult>()
            where TResult : class
        {
            return To<TResult, AutomaticProjection<TSource, TResult>>();
        }

        /// <summary>
        /// Allows to project each element of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by the function.</typeparam>
        /// <typeparam name="TProjection">The projection used to project elements.</typeparam>
        public IQueryable<TResult> To<TResult, TProjection>()
            where TResult : class             
            where TProjection : Projection<TSource, TResult>, new()
        {
            var projection = new TProjection { Source = source };
            return source.Select(projection.GetExpression());
        }
    }
}