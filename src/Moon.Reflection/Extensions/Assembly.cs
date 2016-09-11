using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Moon.Reflection
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Gets the types defined in the assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="predicate">A function to test each type for a condition.</param>
        public static IEnumerable<TypeInfo> GetTypes(this Assembly assembly, Func<TypeInfo, bool> predicate)
            => assembly.DefinedTypes.Where(predicate);
    }
}