using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Moon.Reflection
{
    /// <summary>
    /// Utility for dealing with anonymous objects.
    /// </summary>
    public static class AnonymousObject
    {
        /// <summary>
        /// Converts the anonymous object into the dictionary.
        /// </summary>
        /// <param name="obj">The anonymous object to be converted.</param>
        public static IDictionary<string, object> ToDictionary(object obj)
        {
            Requires.NotNull(obj, nameof(obj));

            if (obj is IDictionary<string, object> dict)
            {
                return dict;
            }

            return obj.GetType().GetRuntimeProperties()
                .ToDictionary(p => p.Name, p => p.GetValue(obj));
        }
    }
}