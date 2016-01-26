using System;
using System.Threading.Tasks;

namespace Moon.Testing
{
    /// <summary>
    /// <see cref="string" /> extension methods.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Defines a step in the current scenario.
        /// </summary>
        /// <param name="step">The description of th step.</param>
        /// <param name="body">The action that will perform the step.&gt;</param>
        public static void x(this string step, Action body)
            => body();

        /// <summary>
        /// Defines a step in the current scenario.
        /// </summary>
        /// <param name="step">The description of th step.</param>
        /// <param name="body">The action that will perform the step.&gt;</param>
        public static void x(this string step, Func<Task> body)
            => body().Wait();
    }
}