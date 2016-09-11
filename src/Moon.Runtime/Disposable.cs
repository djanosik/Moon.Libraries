using System;

namespace Moon
{
    /// <summary>
    /// Helper class providing safe way to dispose objects.
    /// </summary>
    public static class Disposable
    {
        /// <summary>
        /// Disposes the <paramref name="obj" /> if it implements <see cref="IDisposable" /> interface.
        /// </summary>
        /// <param name="obj">The object to be disposed.</param>
        public static void Dispose(object obj)
            => Dispose(obj as IDisposable);

        /// <summary>
        /// Disposes the <paramref name="disposable" /> object if it is not <c>null</c>.
        /// </summary>
        /// <param name="disposable">The object to be disposed.</param>
        public static void Dispose(IDisposable disposable)
            => disposable?.Dispose();
    }
}