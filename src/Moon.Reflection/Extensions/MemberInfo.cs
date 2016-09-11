using System;
using System.Reflection;

namespace Moon.Reflection
{
    public static class MemberInfoExtensions
    {
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