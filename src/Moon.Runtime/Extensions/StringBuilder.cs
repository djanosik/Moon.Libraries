using System.Net;
using System.Text;

namespace Moon
{
    /// <summary>
    /// <see cref="StringBuilder" /> extension methods.
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Appends a URI query parameter to the end of this instance.
        /// </summary>
        /// <param name="source">The string builder to modify.</param>
        /// <param name="key">The query param key.</param>
        /// <param name="value">The query param value.</param>
        /// <param name="urlEncode">Indicates whether to URL encode the value.</param>
        public static StringBuilder AppendQuery(this StringBuilder source, string key, object value, bool urlEncode = true)
        {
            Requires.NotNull(key, nameof(key));

            bool hasQuery = false;

            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == '?')
                {
                    hasQuery = true;
                    break;
                }
            }

            string delim;
            if (!hasQuery)
            {
                delim = "?";
            }
            else if ((source[source.Length - 1] == '?')
                || (source[source.Length - 1] == '&'))
            {
                delim = string.Empty;
            }
            else
            {
                delim = "&";
            }

            var strKey = key;

            if (urlEncode)
            {
                strKey = WebUtility.UrlEncode(key);
            }

            source.Append(delim).Append(strKey);

            if (value != null)
            {
                var strValue = value.ToString();

                if (urlEncode)
                {
                    strValue = WebUtility.UrlEncode(strValue);
                }

                return source.Append("=").Append(strValue);
            }

            return source;
        }
    }
}