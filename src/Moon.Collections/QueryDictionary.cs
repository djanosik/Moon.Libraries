using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Moon.Collections
{
    /// <summary>
    /// Represents a collection of query-string keys and values.
    /// </summary>
    public class QueryDictionary : Dictionary<string, string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryDictionary" /> class.
        /// </summary>
        public QueryDictionary()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryDictionary" /> class.
        /// </summary>
        /// <param name="query">The query string to be parsed.</param>
        /// <param name="urlEncoded">Indicates whether the query string is URL encoded.</param>
        public QueryDictionary(string query, bool urlEncoded = true)
        {
            Requires.NotNull(query, nameof(query));

            ParseQueryString(query, urlEncoded);
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance. The result will not
        /// include question mark.
        /// </summary>
        public override string ToString()
            => ToString(false);

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="includeQuestionMark">
        /// Indicates whether leading question mark should be included.
        /// </param>
        /// <param name="urlEncode">Indicates whether to URL encode the query string.</param>
        public virtual string ToString(bool includeQuestionMark, bool urlEncode = true)
        {
            if (Count == 0)
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            foreach (var pair in this)
            {
                builder.AppendQuery(pair.Key, pair.Value, urlEncode);
            }

            if (!includeQuestionMark)
            {
                builder.Remove(0, 1);
            }

            return builder.ToString();
        }

        void ParseQueryString(string query, bool urlEncoded)
        {
            var questionMarkIndex = query.IndexOf('?');

            if (questionMarkIndex >= 0)
            {
                query = query.Remove(0, questionMarkIndex + 1);
            }

            var length = query.Length;

            for (var i = 0; i < length; i++)
            {
                var startIndex = i;
                var position = -1;

                while (i < length)
                {
                    var ch = query[i];

                    if (ch == '=')
                    {
                        if (position < 0)
                        {
                            position = i;
                        }
                    }
                    else if (ch == '&')
                    {
                        break;
                    }

                    i++;
                }

                string key;
                var value = string.Empty;

                if (position >= 0)
                {
                    key = query.Substring(startIndex, position - startIndex);
                    value = query.Substring(position + 1, (i - position) - 1);
                }
                else
                {
                    key = query.Substring(startIndex, i - startIndex);
                }

                if (urlEncoded)
                {
                    this[WebUtility.UrlDecode(key)] = WebUtility.UrlDecode(value);
                }
                else
                {
                    this[key] = value;
                }
            }
        }
    }
}