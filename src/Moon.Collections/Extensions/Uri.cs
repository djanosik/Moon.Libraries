using System;

namespace Moon.Collections
{
    /// <summary>
    /// <see cref="Uri" /> extension methods.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Parses the query string and returns a <see cref="QueryDictionary" />.
        /// </summary>
        /// <param name="uri">The <see cref="Uri" /> instance.</param>
        public static QueryDictionary GetQuery(this Uri uri)
            => new QueryDictionary(uri.Query);
    }
}