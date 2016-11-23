using System;

namespace Moon.Collections
{
    public static class UriExtensions
    {
        /// <summary>
        /// Parses the query string and returns a <see cref="QueryDictionary" />.
        /// </summary>
        /// <param name="uri">The <see cref="Uri" /> instance.</param>
        public static QueryDictionary GetQuery(this Uri uri)
            => new QueryDictionary(uri.Query);

        /// <summary>
        /// Returns a value of the query parameter with the specified key.
        /// </summary>
        /// <param name="uri">The <see cref="UriBuilder" /> instance.</param>
        /// <param name="key">The query parameter key.</param>
        public static string GetQuery(this Uri uri, string key)
            => uri.GetQuery()[key];
    }
}