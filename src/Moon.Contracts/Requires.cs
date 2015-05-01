using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Moon
{
    /// <summary>
    /// Simple Guard (or DbC) implementation. Each method throws a <see cref="ContractException" />
    /// if the contract is broken.
    /// </summary>
    public static class Requires
    {
        /// <summary>
        /// Requires that an instance of the <paramref name="type" /> can be assigned to a variable
        /// of the <typeparamref name="TTarget" /> type.
        /// </summary>
        /// <typeparam name="TTarget">The target type.</typeparam>
        /// <param name="type">The type to check.</param>
        /// <param name="name">The expression being tested.</param>
        public static void AssignableTo<TTarget>(Type type, string name)
        {
            var targetType = typeof(TTarget);

            That(targetType.GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()),
                $"{name} is not assignable to type {targetType.Name}");
        }

        /// <summary>
        /// Requires that the <paramref name="collection" /> is not empty.
        /// </summary>
        /// <param name="collection">The value to check.</param>
        /// <param name="name">The expression being tested.</param>
        public static void NotEmpty(IEnumerable<object> collection, string name)
            => That(collection.Any(), $"{name} contains no elements");

        /// <summary>
        /// Requires that the <paramref name="value" /> is not <c>null</c>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="name">The expression being tested.</param>
        public static void NotNull(object value, string name)
            => That(value != null, $"{name} is null");

        /// <summary>
        /// Requires that the <paramref name="value" /> is not <c>null</c> or empty.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="name">The expression being tested.</param>
        public static void NotNullOrEmpty(string value, string name)
            => That(!string.IsNullOrEmpty(value), $"{name} is null or empty");

        /// <summary>
        /// Requires that the <paramref name="value" /> is not <c>null</c>, empty and doesn't
        /// consist only of white-space characters.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <param name="name">The expression being tested.</param>
        public static void NotNullOrWhiteSpace(string value, string name)
            => That(!string.IsNullOrWhiteSpace(value), $"{name} is null, empty or consists only of white-space characters");

        /// <summary>
        /// Requires that the <paramref name="condition" /> is <c>true</c>; if it is false, throws
        /// an exception with the specified message.
        /// </summary>
        /// <param name="condition">The condition to be checked.</param>
        /// <param name="message">The message of the exception thrown when the condition fails.</param>
        /// <exception cref="ContractException">The <paramref name="condition" /> is false.</exception>
        public static void That(bool condition, string message = null)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                message = "precondition failed";
            }

            if (!condition)
            {
                throw new ContractException(message);
            }
        }
    }
}