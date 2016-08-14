using System;
using System.Linq;

namespace Moon.Collections
{
    /// <summary>
    /// <see cref="Array" /> extension methods.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Moves an item at the <paramref name="oldIndex" /> to the <paramref name="newIndex" />.
        /// </summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">The array to be modified.</param>
        /// <param name="oldIndex">The old index of an element.</param>
        /// <param name="newIndex">The new index of an element.</param>
        public static void Move<T>(this T[] source, int oldIndex, int newIndex)
        {
            Requires.That((oldIndex >= 0) && (oldIndex < source.Count()), "oldIndex is out of range");
            Requires.That((newIndex >= 0) && (newIndex < source.Count()), "newIndex is out of range");

            if (oldIndex != newIndex)
            {
                var tmp = source[oldIndex];
                source[oldIndex] = source[newIndex];
                source[newIndex] = tmp;
            }
        }

        /// <summary>
        /// Shrinks an array to a given length if it's not already of that length, and returns the array.
        /// </summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">The array to be modified.</param>
        /// <param name="length">The desired length of the array.</param>
        public static T[] Trim<T>(this T[] source, int length)
        {
            if ((uint)length > (uint)source.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (length != source.Length)
            {
                var trimmed = new T[length];
                Array.Copy(source, trimmed, length);
                source = trimmed;
            }

            return source;
        }
    }
}