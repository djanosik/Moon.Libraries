using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Moon.Reflection
{
    /// <summary>
    /// Utilities for dealing with objects.
    /// </summary>
    public static class Anonymous
    {
        /// <summary>
        /// Converts the anonymous object into the dictionary.
        /// </summary>
        /// <param name="obj">The anonymous object to be converted.</param>
        public static IDictionary<string, string> ToDictionary(object obj)
        {
            Requires.NotNull(obj, nameof(obj));

            return obj.GetType().GetRuntimeProperties()
                .ToDictionary(p => p.Name, p => p.GetValue(obj).ToString());
        }
    }
}