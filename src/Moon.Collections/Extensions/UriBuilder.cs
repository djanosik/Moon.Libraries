using System;

namespace Moon.Collections
{
    /// <summary>
    /// <see cref="UriBuilder" /> extension methods.
    /// </summary>
    public static class UriBuilderExtensions
    {
        /// <summary>
        /// Returns a value of the query parameter with the specified key.
        /// </summary>
        /// <param name="uri">The <see cref="UriBuilder" /> instance.</param>
        /// <param name="key">The query parameter key.</param>
        public static string GetQuery(this UriBuilder uri, string key)
            => uri.GetQuery()[key];

        /// <summary>
        /// Parses the query string and returns a <see cref="QueryCollection" />.
        /// </summary>
        /// <param name="uri">The <see cref="UriBuilder" /> instance.</param>
        public static QueryDictionary GetQuery(this UriBuilder uri)
            => new QueryDictionary(uri.Query);

        /// <summary>
        /// Removes the query.
        /// </summary>
        /// <param name="uri">The <see cref="UriBuilder" /> instance.</param>
        /// <param name="key">The query parameter key.</param>
        public static UriBuilder RemoveQuery(this UriBuilder uri, string key)
        {
            var query = uri.GetQuery();
            query.Remove(key);
            uri.Query = query.ToString();
            return uri;
        }

        /// <summary>
        /// Sets the specified query parameter. If the key already exists, the value is overwritten.
        /// </summary>
        /// <param name="uri">The <see cref="UriBuilder" /> instance.</param>
        /// <param name="key">The query parameter key.</param>
        /// <param name="value">The query parameter value.</param>
        public static UriBuilder SetQuery(this UriBuilder uri, string key, string value)
        {
            var query = uri.GetQuery();
            query[key] = value;
            uri.Query = query.ToString();
            return uri;
        }
    }
}