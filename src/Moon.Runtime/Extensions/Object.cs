using System.Collections.Generic;

namespace Moon
{
    /// <summary>
    /// <see cref="object" /> extension methods.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Returns a string that represents the current object converted to lowercase.
        /// </summary>
        /// <param name="obj">The object to convert to string.</param>
        public static string ToLowerString(this object obj)
            => obj.ToString().ToLower();

        /// <summary>
        /// Returns a string that represents the current object converted to uppercase.
        /// </summary>
        /// <param name="obj">The object to convert to string.</param>
        public static string ToUpperString(this object obj)
            => obj.ToString().ToUpper();

        /// <summary>
        /// Returns the object as an enumerable with one element. If the object is <c>null</c>,
        /// empty sequence is returned.
        /// </summary>
        /// <typeparam name="TObj">The type of the object.</typeparam>
        /// <param name="obj">The object to return as enumerable.</param>
        public static IEnumerable<TObj> Yield<TObj>(this TObj obj)
        {
            if (!Equals(obj, null))
            {
                yield return obj;
            }
        }
    }
}