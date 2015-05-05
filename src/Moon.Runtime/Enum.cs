using System;
using System.Collections.Generic;
using System.Linq;

namespace Moon
{
    /// <summary>
    /// Generic Enum helper.
    /// </summary>
    /// <typeparam name="TEnum">The enum type.</typeparam>
    public static class Enum<TEnum> where TEnum : struct
    {
        /// <summary>
        /// Determines whether the specified <typeparamref name="TEnum" /> instances are considered equal.
        /// </summary>
        /// <param name="x">The first instance to compare.</param>
        /// <param name="y">The second instance to compare.</param>
        public static bool Equals(TEnum x, TEnum y)
            => object.Equals(x, y);

        /// <summary>
        /// Converts the specified value of a specified enumerated type to its equivalent string
        /// representation according to the specified format.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="format">The output format to use.</param>
        public static string Format(TEnum value, string format)
            => Enum.Format(typeof(TEnum), value, format);

        /// <summary>
        /// Enumerates all items defined in a specified enumeration.
        /// </summary>
        public static IEnumerable<TEnum> GetAllItems()
            => GetValues().Cast<TEnum>();

        /// <summary>
        /// Returns the name of the constant in the specified enumeration that has the specified value.
        /// </summary>
        /// <param name="value">
        /// The value of a particular enumerated constant in terms of its underlying type.
        /// </param>
        public static string GetName(object value)
            => Enum.GetName(typeof(TEnum), value);

        /// <summary>
        /// Returns an array of the names of the constants in a specified enumeration.
        /// </summary>
        public static IEnumerable<string> GetNames()
            => Enum.GetNames(typeof(TEnum));

        /// <summary>
        /// Returns the underlying type of the specified enumeration.
        /// </summary>
        public static Type GetUnderlyingType()
            => Enum.GetUnderlyingType(typeof(TEnum));

        /// <summary>
        /// Returns an array of the values of the constants in a specified enumeration.
        /// </summary>
        public static Array GetValues()
            => Enum.GetValues(typeof(TEnum));

        /// <summary>
        /// Returns an indication whether a constant with the specified value or name exists in a
        /// specified enumeration.
        /// </summary>
        /// <param name="value">The value or name of a constant.</param>
        public static bool IsDefined(object value)
            => Enum.IsDefined(typeof(TEnum), value);

        /// <summary>
        /// Returns an indication whether a constant with the specified name exists in a specified enumeration.
        /// </summary>
        /// <param name="name">The name of a constant.</param>
        /// <param name="ignoreCase">If true, ignore case; otherwise, regard case.</param>
        public static bool IsDefined(string name, bool ignoreCase)
        {
            Requires.NotNull(name, nameof(name));

            if (!ignoreCase)
            {
                return IsDefined(name);
            }

            var names = GetNames().Select(i => i.ToLower());
            return names.Contains(name.ToLower());
        }

        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more
        /// enumerated constants to an equivalent enumerated object.
        /// </summary>
        /// <param name="value">A string containing the name or value to convert.</param>
        public static TEnum Parse(string value)
            => (TEnum)Enum.Parse(typeof(TEnum), value);

        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more
        /// enumerated constants to an equivalent enumerated object. A parameter specifies whether
        /// the operation is case-sensitive.
        /// </summary>
        /// <param name="value">
        /// The string representation of the enumeration name or underlying value to convert.
        /// </param>
        /// <param name="ignoreCase">If true, ignore case; otherwise, regard case.</param>
        public static TEnum Parse(string value, bool ignoreCase)
            => (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);

        /// <summary>
        /// Returns an instance of the enumeration set to the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public static TEnum ToObject(object value)
            => (TEnum)Enum.ToObject(typeof(TEnum), value);

        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more
        /// enumerated constants to an equivalent enumerated object. The return value indicates
        /// whether the conversion succeeded.
        /// </summary>
        /// <param name="value">
        /// The string representation of the enumeration name or underlying value to convert.
        /// </param>
        /// <param name="result">When this method returns, contains an object of type <typeparamref name="TEnum" />.</param>
        public static bool TryParse(string value, out TEnum result)
            => Enum.TryParse<TEnum>(value, out result);

        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more
        /// enumerated constants to an equivalent enumerated object. The return value indicates
        /// whether the conversion succeeded.
        /// </summary>
        /// <param name="value">
        /// The string representation of the enumeration name or underlying value to convert.
        /// </param>
        /// <param name="ignoreCase">If true, ignore case; otherwise, regard case.</param>
        /// <param name="result">When this method returns, contains an object of type <typeparamref name="TEnum" />.</param>
        public static bool TryParse(string value, bool ignoreCase, out TEnum result)
            => Enum.TryParse<TEnum>(value, ignoreCase, out result);
    }
}