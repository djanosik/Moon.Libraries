using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Moon.Reflection
{
    public static class TypeInfoExtensions
    {
        /// <summary>
        /// Searches for a public instance constructor whose parameters match the types in the
        /// specified enumeration.
        /// </summary>
        /// <param name="typeInfo">The <see cref="TypeInfo" /> instance.</param>
        /// <param name="types">
        /// An enumeration of types representing the number, order, and type of the parameters for
        /// the desired constructor.
        /// </param>
        public static ConstructorInfo GetConstructor(this TypeInfo typeInfo, IEnumerable<Type> types)
        {
            Requires.NotNull(types, nameof(types));

            ConstructorInfo result = null;
            var argTypes = types.ToArray();

            foreach (var ctor in typeInfo.DeclaredConstructors)
            {
                if (!ctor.IsPublic)
                {
                    continue;
                }

                var paramTypes = ctor.GetParameters()
                    .Select(x => x.ParameterType)
                    .ToArray();

                if (argTypes.Length != paramTypes.Length)
                {
                    continue;
                }

                for (var i = 0; i < argTypes.Length; i++)
                {
                    if (argTypes[i] != paramTypes[i])
                    {
                        break;
                    }

                    if (i == argTypes.Length - 1)
                    {
                        result = ctor;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Determines whether the type is implementation of the specified interface.
        /// </summary>
        /// <param name="typeInfo">The <see cref="TypeInfo" /> instance.</param>
        /// <param name="interfaceInfo">The interface type.</param>
        public static bool Implements(this TypeInfo typeInfo, TypeInfo interfaceInfo)
        {
            Requires.NotNull(interfaceInfo, nameof(interfaceInfo));
            Requires.That(interfaceInfo.IsInterface, "interfaceInfo is not interface");

            if (interfaceInfo.IsGenericTypeDefinition)
            {
                return typeInfo.ImplementedInterfaces
                    .Any(x => x.GetTypeInfo().IsGenericType && (x.GetTypeInfo().GetGenericTypeDefinition() == interfaceInfo.AsType()));
            }

            return interfaceInfo.IsAssignableFrom(typeInfo);
        }
    }
}