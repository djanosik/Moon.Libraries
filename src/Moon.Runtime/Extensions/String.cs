using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moon
{
    /// <summary>
    /// <see cref="string" /> extension methods.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Returns the string capitalized.
        /// </summary>
        /// <param name="input">The string to capitalize.</param>
        public static string Capitalize(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return char.ToUpper(input[0]) + input.Substring(1);
            }

            return input;
        }

        /// <summary>
        /// Returns a value indicating whether the specified <see cref="String" /> object occurs
        /// within this string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="comparisonType">
        /// One of the enumeration values that specifies the rules for the search.
        /// </param>
        public static bool Contains(this string input, string value, StringComparison comparisonType)
            => input.IndexOf(value, comparisonType) != -1;

        /// <summary>
        /// Returns a value indicating whether the specified <see cref="Char" /> object occurs
        /// within this string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="value">The character to seek.</param>
        public static bool Contains(this string input, char value)
            => input.IndexOf(value) != -1;

        /// <summary>
        /// Removes diacritics from the string.
        /// </summary>
        /// <param name="input">The string to modify.</param>
        public static string RemoveDiacritics(this string input)
        {
            var bytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(input);
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Removes all occurrences of the specified string.
        /// </summary>
        /// <param name="input">The string to modify.</param>
        /// <param name="str">The string to be removed.</param>
        /// <param name="comparisonType">The type of string comparison.</param>
        public static string RemoveAll(this string input, string str, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            if (string.IsNullOrEmpty(str))
            {
                return input;
            }

            while (input.IndexOf(str, comparisonType) >= 0)
            {
                input = input.RemoveFirst(str, comparisonType);
            }

            return input;
        }

        /// <summary>
        /// Removes the first occurrence of the specified string.
        /// </summary>
        /// <param name="input">The string to modify.</param>
        /// <param name="str">The string to be removed.</param>
        /// <param name="comparisonType">The type of string comparison.</param>
        public static string RemoveFirst(this string input, string str, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            if (string.IsNullOrEmpty(str))
            {
                return input;
            }

            var length = str.Length;
            var position = input.IndexOf(str, comparisonType);

            if (position < 0)
            {
                return input;
            }

            return input.Remove(position, length);
        }

        /// <summary>
        /// Removes the last occurrence of the specified string.
        /// </summary>
        /// <param name="input">The string to modify.</param>
        /// <param name="str">The string to be removed.</param>
        /// <param name="comparisonType">The type of string comparison.</param>
        public static string RemoveLast(this string input, string str, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            if (string.IsNullOrEmpty(str))
            {
                return input;
            }

            var length = str.Length;
            var position = input.LastIndexOf(str, comparisonType);

            if (position < 0)
            {
                return input;
            }

            return input.Remove(position, length);
        }

        /// <summary>
        /// Returns a new string in which all occurrences of a specified string in the current
        /// instance are replaced with another specified string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="oldValue">The string to be replaced.</param>
        /// <param name="newValue">The string to replace all occurrences of oldValue.</param>
        /// <param name="comparisonType">
        /// One of the enumeration values that specifies the rules for the search.
        /// </param>
        public static string Replace(this string input, string oldValue, string newValue, StringComparison comparisonType)
        {
            var builder = new StringBuilder();

            var previousIndex = 0;
            var index = input.IndexOf(oldValue, comparisonType);

            while (index != -1)
            {
                builder.Append(input.Substring(previousIndex, index - previousIndex));
                builder.Append(newValue);

                index += oldValue.Length;

                previousIndex = index;
                index = input.IndexOf(oldValue, index, comparisonType);
            }
            builder.Append(input.Substring(previousIndex));

            return builder.ToString();
        }

        /// <summary>
        /// Shortens the text to the specified length.
        /// </summary>
        /// <param name="input">The string to shorten.</param>
        /// <param name="length">The length of the result string.</param>
        public static string Shorten(this string input, int length)
        {
            var delimiters = new[] {
                " ", ".", ",", ";", "\\", "/", "(", ")", ":", "-", "="
            };

            return input.Shorten(length, delimiters);
        }

        /// <summary>
        /// Shortens the text to the specified length.
        /// </summary>
        /// <param name="input">The string to shorten.</param>
        /// <param name="length">The length of the result string.</param>
        /// <param name="delimiters">The text delimiters.</param>
        public static string Shorten(this string input, int length, IEnumerable<string> delimiters)
        {
            string finalText;
            delimiters = delimiters.ToArray();
            input = input.Trim();

            if (input.Length > length)
            {
                var lastChar = input.Substring(length, 1);

                if (delimiters.Contains(lastChar))
                {
                    finalText = input.Substring(0, length);
                }
                else
                {
                    finalText = input.Substring(0, length);

                    for (var i = length - 1; i >= 0; i--)
                    {
                        lastChar = input.Substring(i, 1);

                        if (delimiters.Contains(lastChar))
                        {
                            finalText = input.Substring(0, i);
                            break;
                        }
                    }
                }

                finalText = finalText + "…";
            }
            else
            {
                finalText = input;
            }

            return finalText;
        }

        /// <summary>
        /// Splits a string by the given substring.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="removeEmptyEntries">
        /// Indicates whether to omit empty array elements from the array returned.
        /// </param>
        public static string[] Split(this string input, string separator, bool removeEmptyEntries = false)
            => input.Split(new[] { separator }, removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);

        /// <summary>
        /// Replaces the format item in the string with the string representation of a corresponding
        /// object in the specified array.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static string With(this string format, params object[] args)
            => string.Format(format, args);
    }
}