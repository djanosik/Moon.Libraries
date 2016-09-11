using System.Collections.Generic;

namespace Moon.Collections
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds a list of items to an <see cref="ICollection{T}" />.
        /// </summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="items">An array of items to add to the collection.</param>
        public static void AddRange<T>(this ICollection<T> source, params T[] items)
            => AddRange(source, (IEnumerable<T>)items);

        /// <summary>
        /// Adds a list of items to an <see cref="ICollection{T}" />.
        /// </summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="items">An enumeration of items to add to the collection.</param>
        public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> items)
        {
            if (items != null)
            {
                var list = source as List<T>;

                if (list != null)
                {
                    list.AddRange(items);
                }
                else
                {
                    foreach (var item in items)
                    {
                        source.Add(item);
                    }
                }
            }
        }
    }
}