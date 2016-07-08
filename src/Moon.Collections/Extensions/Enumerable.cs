using System;
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
        /// Determines whether a sequence contains no elements.
        /// </summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">A source enumeration.</param>
        public static bool Empty<T>(this IEnumerable<T> source)
            => !source.Any();

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
        /// Projects each element of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by selector.</typeparam>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>
        /// A <see cref="List{T}" /> whose elements are the result of invoking the transform
        /// function on each element of source.
        /// </returns>
        public static List<TResult> List<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
            => source.Select(selector).ToList();

        /// <summary>
        /// Projects each element of a sequence into a new form by incorporating the element's index.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by selector.</typeparam>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>
        /// A <see cref="List{T}" /> whose elements are the result of invoking the transform
        /// function on each element of source.
        /// </returns>
        public static List<TResult> List<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
            => source.Select(selector).ToList();

        /// <summary>
        /// Returns first half of items from the <paramref name="source" /> enumeration.
        /// </summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">A source enumeration.</param>
        public static IEnumerable<T> FirstHalf<T>(this IEnumerable<T> source)
        {
            var items = source.ToArray();
            T[] results;

            var isLengthEven = items.Length % 2 == 0;
            var halfLength = items.Length / 2;

            if (isLengthEven)
            {
                results = new T[halfLength];
                Array.Copy(items, results, halfLength);
            }
            else
            {
                results = new T[halfLength + 1];
                Array.Copy(items, results, halfLength + 1);
            }

            return results;
        }

        /// <summary>
        /// Returns second half of items from the <paramref name="source" /> enumeration.
        /// </summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">A source enumeration.</param>
        public static IEnumerable<T> SecondHalf<T>(this IEnumerable<T> source)
        {
            var items = source.Reverse().ToArray();
            var halfLength = items.Length / 2;
            var results = new T[halfLength];

            Array.Copy(items, results, halfLength);

            return results.Reverse();
        }

        /// <summary>
        /// Performs the specified action for all elements.
        /// </summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}" /> to iterate through.</param>
        /// <param name="action">The action to invoke for each item.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            Requires.NotNull(action, nameof(action));

            foreach (var item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// Returns the items in ascending order, with their keys compared using the given <see cref="Comparison{T}" />.
        /// </summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">A source enumeration.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <param name="comparison">A function to compare keys.</param>
        public static IOrderedEnumerable<T> OrderBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector, Comparison<TKey> comparison)
            => source.OrderBy(keySelector, new DelegateComparer<TKey>(comparison));

        /// <summary>
        /// Returns the items in descending order, with their keys compared using the given <see cref="Comparison{T}" />.
        /// </summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">A source enumeration.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <param name="comparison">A function to compare keys.</param>
        public static IOrderedEnumerable<T> OrderByDescending<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector, Comparison<TKey> comparison)
            => source.OrderByDescending(keySelector, new DelegateComparer<TKey>(comparison));

        /// <summary>
        /// Returns a <see cref="HashSet{T}" /> containing the items from the given sequence.
        /// </summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">A source enumeration.</param>
        public static HashSet<T> ToSet<T>(this IEnumerable<T> source)
            => new HashSet<T>(source);
    }
}