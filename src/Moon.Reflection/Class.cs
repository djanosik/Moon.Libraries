using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Moon.Reflection
{
    /// <summary>
    /// Provides fast way to create instance of unknown type.
    /// </summary>
    public static class Class
    {
        /// <summary>
        /// Creates an instance of the specified type.
        /// </summary>
        /// <typeparam name="TResult">The <see cref="Type" /> of the result.</typeparam>
        public static TResult Activate<TResult>() where TResult : class, new()
            => (TResult)Activate(typeof(TResult));

        /// <summary>
        /// Creates an instance of the specified type.
        /// </summary>
        /// <typeparam name="TResult">The <see cref="Type" /> of the result.</typeparam>
        /// <param name="args">An array of constructor arguments.</param>
        public static TResult Activate<TResult>(params object[] args) where TResult : class
            => (TResult)Activate(typeof(TResult), args);

        /// <summary>
        /// Creates an instance of the specified type.
        /// </summary>
        /// <typeparam name="TResult">The <see cref="Type" /> of the result.</typeparam>
        /// <param name="type">The <see cref="Type" /> to create an instance of.</param>
        /// <param name="args">An array of constructor arguments.</param>
        public static TResult Activate<TResult>(Type type, params object[] args)
        {
            Requires.AssignableTo<TResult>(type, nameof(type));

            return (TResult)Activate(type, args);
        }

        /// <summary>
        /// Creates an instance of the specified type.
        /// </summary>
        /// <param name="type">The <see cref="Type" /> of the result object.</param>
        /// <param name="args">An array of constructor arguments.</param>
        public static object Activate(Type type, params object[] args)
        {
            Requires.NotNull(type, nameof(type));

            return args.Any() ? GetActivator(type, args)(args) : GetActivator(type)();
        }

        private static Func<object[], object> GetActivator(Type type, IEnumerable<object> args)
        {
            var argTypes = args.Select(x => x.GetType()).ToArray();
            var ctorInfo = type.GetTypeInfo().GetConstructor(argTypes);

            if (ctorInfo == null)
            {
                throw new ArgumentException("Constructor with the specified number, order, and type of parameters was not found!");
            }

            var argValues = Expression.Parameter(typeof(object[]));
            var ctorArguments = GetConstructorArguments(ctorInfo, argValues);

            var lambda = Expression.Lambda(typeof(Func<object[], object>), Expression.New(ctorInfo, ctorArguments), argValues);
            return (Func<object[], object>)lambda.Compile();
        }

        private static Func<object> GetActivator(Type type)
        {
            return (Func<object>)Expression.Lambda(Expression.Convert(Expression.New(type), typeof(object))).Compile();
        }

        private static Expression[] GetConstructorArguments(ConstructorInfo ctorInfo, ParameterExpression argValues)
        {
            var argTypes = ctorInfo.GetParameters().Select(x => x.ParameterType).ToArray();
            var ctorArguments = new Expression[argTypes.Length];

            for (var i = 0; i < argTypes.Length; i++)
            {
                var argAccessor = Expression.ArrayIndex(argValues, Expression.Constant(i));
                ctorArguments[i] = Expression.Convert(argAccessor, argTypes[i]);
            }

            return ctorArguments;
        }
    }
}