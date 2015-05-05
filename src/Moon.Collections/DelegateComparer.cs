using System;
using System.Collections.Generic;

namespace Moon.Collections
{
    /// <summary>
    /// Compares two values by using a <see cref="Comparison{T}" /> delegate.
    /// </summary>
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    public sealed class DelegateComparer<T> : IComparer<T>
    {
        private readonly Comparison<T> comparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateComparer{T}" /> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        public DelegateComparer(Comparison<T> comparer)
        {
            Requires.NotNull(comparer, nameof(comparer));

            this.comparer = comparer;
        }

        /// <summary>
        /// Compares two objects by using the <see cref="Comparison{T}" /> delegate and returns a
        /// value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        public int Compare(T x, T y)
            => comparer(x, y);
    }
}