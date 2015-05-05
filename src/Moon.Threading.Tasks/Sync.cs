using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moon.Threading.Tasks
{
    /// <summary>
    /// Runs asynchronous methods synchronously.
    /// </summary>
    public class Sync
    {
        private static readonly TaskFactory factory;

        /// <summary>
        /// Initializes the <see cref="Sync" /> class.
        /// </summary>
        static Sync()
        {
            factory = new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);
        }

        /// <summary>
        /// Runs the given task synchronously and returns the result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="task">The task to run synchronously.</param>
        public static TResult Run<TResult>(Func<Task<TResult>> task)
        {
            Requires.NotNull(task, nameof(task));

            var innerTask = factory.StartNew(task);

            return innerTask.Unwrap().GetAwaiter()
                .GetResult();
        }

        /// <summary>
        /// Runs the given task synchronously.
        /// </summary>
        /// <param name="task">The task to run synchronously.</param>
        public static void Run(Func<Task> task)
        {
            Requires.NotNull(task, nameof(task));

            var innerTask = factory.StartNew(task);

            innerTask.Unwrap().GetAwaiter()
                .GetResult();
        }
    }
}