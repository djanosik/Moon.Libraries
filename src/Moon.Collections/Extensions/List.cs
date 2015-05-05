using System;
using System.Collections.Generic;
using System.Linq;

namespace Moon.Collections
{
    /// <summary>
    /// <see cref="IList" /> extension methods.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Adds a list of items to a <see cref="List{T}" />.
        /// </summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">The source list.</param>
        /// <param name="items">An array of items to add to the collection.</param>
        public static void AddRange<T>(this List<T> source, params T[] items)
        {
            if (items != null)
            {
                source.AddRange(items.AsEnumerable());
            }
        }

        /// <summary>
        /// Returns empty list if the <paramref name="source" /> is <c>null</c>.
        /// </summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">A source list.</param>
        public static List<T> EmptyIfNull<T>(this List<T> source)
            => source ?? new List<T>(0);

        /// <summary>
        /// Returns empty list if the <paramref name="source" /> is <c>null</c>.
        /// </summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">A source list.</param>
        public static IList<T> EmptyIfNull<T>(this IList<T> source)
            => source ?? new List<T>(0);

        /// <summary>
        /// Moves na item at the <paramref name="oldIndex" /> to the <paramref name="newIndex" />.
        /// </summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">The list to be modified.</param>
        /// <param name="oldIndex">The old index of an element.</param>
        /// <param name="newIndex">The new index of an element.</param>
        public static void Move<T>(this IList<T> source, int oldIndex, int newIndex)
        {
            Requires.That(oldIndex >= 0 && oldIndex < source.Count(), "oldIndex is out of range");
            Requires.That(newIndex >= 0 && newIndex < source.Count(), "newIndex is out of range");

            if (oldIndex != newIndex)
            {
                T item = source[oldIndex];
                source.RemoveAt(oldIndex);
                source.Insert(newIndex, item);
            }
        }

        /// <summary>
        /// Removes duplicates from the list. Note that this method has O(dn+n) complexity (where d
        /// is the number of
        /// duplicates) and is unsuitable for lists containing large numbers of items and duplicates.
        /// </summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">The source list.</param>
        public static void RemoveDuplicates<T>(this IList<T> source)
            => source.RemoveDuplicates(EqualityComparer<T>.Default);

        /// <summary>
        /// Removes duplicates from the list. Note that this method has O(dn+n) complexity (where d
        /// is the number of
        /// duplicates) and is unsuitable for lists containing large numbers of items and duplicates.
        /// </summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">The source list.</param>
        /// <param name="comparer">The <see cref="IEqualityComparer{T}" /> to compare elements.</param>
        public static void RemoveDuplicates<T>(this IList<T> source, IEqualityComparer<T> comparer)
        {
            Requires.NotNull(comparer, nameof(comparer));

            var itemsSeen = new HashSet<T>(comparer);

            for (var i = source.Count - 1; i >= 0; i--)
            {
                T item = source[i];

                if (!itemsSeen.Add(item))
                {
                    source.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Removes duplicates from the list. Note that this method has O(2n^2) complexity and is
        /// unsuitable for lists containing large numbers of items.
        /// </summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">The source list.</param>
        /// <param name="comparer">The function to compare elements.</param>
        public static void RemoveDuplicates<T>(this IList<T> source, Comparison<T> comparer)
            => source.RemoveDuplicates((a, b) => comparer(a, b) == 0);

        /// <summary>
        /// Removes duplicates from the list. Note that this method has O(2n^2) complexity and is
        /// unsuitable for lists containing large numbers of items.
        /// </summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">The source list.</param>
        /// <param name="comparer">The function to compare elements.</param>
        public static void RemoveDuplicates<T>(this IList<T> source, Func<T, T, bool> comparer)
        {
            Requires.NotNull(comparer, nameof(comparer));

            for (var i = source.Count - 1; i >= 1; i--)
            {
                var item = source[i];

                for (var j = i - 1; j >= 0; j--)
                {
                    if (comparer(item, source[j]))
                    {
                        source.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }
}