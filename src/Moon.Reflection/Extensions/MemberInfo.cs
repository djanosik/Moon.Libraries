using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Moon.Reflection
{
    /// <summary>
    /// <see cref="MemberInfo" /> extension methods.
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Returns an attribute of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="member">The member info.</param>
        public static T GetAttribute<T>(this MemberInfo member) where T : Attribute
            => member.GetAttribute<T>(true);

        /// <summary>
        /// Returns an attribute of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="member">The member info.</param>
        /// <param name="inherit">
        /// Specifies whether to search this member's inheritance chain to find the attribute.
        /// </param>
        public static T GetAttribute<T>(this MemberInfo member, bool inherit) where T : Attribute
            => member.GetCustomAttributes(inherit).OfType<T>().FirstOrDefault();

        /// <summary>
        /// Returns attributes of the specified type.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type" /> of attributes.</typeparam>
        /// <param name="member">The member info.</param>
        public static IEnumerable<T> GetAttributes<T>(this MemberInfo member) where T : Attribute
            => member.GetAttributes<T>(true);

        /// <summary>
        /// Returns attributes of the specified type.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type" /> of attributes.</typeparam>
        /// <param name="member">The member info.</param>
        /// <param name="inherit">
        /// Specifies whether to search this member's inheritance chain to find the attributes.
        /// </param>
        public static IEnumerable<T> GetAttributes<T>(this MemberInfo member, bool inherit) where T : Attribute
            => member.GetCustomAttributes(inherit).OfType<T>();

        /// <summary>
        /// Returns an attribute of the specified type.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type" /> of attribute.</typeparam>
        /// <param name="member">The member info.</param>
        public static bool IsDefined<T>(this MemberInfo member) where T : Attribute
            => member.IsDefined<T>(true);

        /// <summary>
        /// Returns whether one or more attributes of the specified type or of its derived types is
        /// applied to this member.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type" /> of attribute.</typeparam>
        /// <param name="member">The member info.</param>
        /// <param name="inherit">
        /// Specifies whether to search this member's inheritance chain to find the attribute.
        /// </param>
        public static bool IsDefined<T>(this MemberInfo member, bool inherit) where T : Attribute
            => member.IsDefined(typeof(T), inherit);
    }
}