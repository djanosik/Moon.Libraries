using System;

namespace Moon
{
    /// <summary>
    /// Utility for creating <see cref="Lazy{T}" /> instances.
    /// </summary>
    public static class Lazy
    {
        /// <summary>
        /// Creates a new instance of the <see cref="Lazy&lt;T&gt;" /> using the specified loader.
        /// </summary>
        /// <typeparam name="T">The type of a lazy loaded object.</typeparam>
        /// <param name="loader">A function used to load an object of type <typeparamref name="T" />.</param>
        public static Lazy<T> From<T>(Func<T> loader)
            => new Lazy<T>(loader);
    }
}