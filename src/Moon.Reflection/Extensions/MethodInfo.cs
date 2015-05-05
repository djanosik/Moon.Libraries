using System.Reflection;

namespace Moon.Reflection
{
    /// <summary>
    /// <see cref="MethodInfo" /> extension methods.
    /// </summary>
    public static class MethodInfoExtensions
    {
        /// <summary>
        /// Invokes the method or constructor represented by the current instance, using the
        /// specified parameters.
        /// </summary>
        /// <param name="method">The method or constructor.</param>
        /// <param name="obj">The object on which to invoke the method or constructor.</param>
        /// <param name="parameters">An argument list for the invoked method or constructor.</param>
        public static void Invoke(this MethodInfo method, object obj, params object[] parameters)
            => method.Invoke(obj, parameters);

        /// <summary>
        /// Invokes the method or constructor represented by the current instance, using the
        /// specified parameters.
        /// </summary>
        /// <param name="method">The method or constructor.</param>
        /// <param name="obj">The object on which to invoke the method or constructor.</param>
        /// <param name="parameters">An argument list for the invoked method or constructor.</param>
        public static TReturn Invoke<TReturn>(this MethodInfo method, object obj, params object[] parameters)
            => (TReturn)method.Invoke(obj, parameters);
    }
}