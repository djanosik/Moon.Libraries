using System.Collections.Generic;
using System.Linq;

namespace Moon.Collections
{
    /// <summary>
    /// <see cref="IEnumerable{T}" /> extension methods.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Appends the specified item to the sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}" /> to append the item to.</param>
        /// <param name="item">The item to append to the source sequence.</param>
        public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> source, TSource item)
        {
            foreach (var sourceItem in source)
            {
                yield return sourceItem;
            }            
            yield return item;
        }

        /// <summary>
        /// Returns whether the sequence is empty.
        /// </summary>
        /// <typeparam name="TSource">The type of elements of source.</typeparam>
        /// <param name="source">The source sequence.</param>
        public static bool Empty<TSource>(this IEnumerable<TSource> source)
            => !source.Any();
    }
}