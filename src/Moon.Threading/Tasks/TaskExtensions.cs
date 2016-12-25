using System;
using System.Threading.Tasks;

namespace Moon.Threading.Tasks
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Creates a continuation that executes asynchronously when the target task completes.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TNewResult">The type of the new result.</typeparam>
        /// <param name="task">The target task.</param>
        /// <param name="continuation">The continuation function.</param>
        public static Task<TNewResult> Then<TResult, TNewResult>(this Task<TResult> task, Func<Task<TResult>, TNewResult> continuation)
            => task.ContinueWith(continuation);

        /// <summary>
        /// Creates a continuation that executes asynchronously when the target task completes.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TNewResult">The type of the new result.</typeparam>
        /// <param name="task">The target task.</param>
        /// <param name="continuation">The continuation function.</param>
        public static Task<TNewResult> Then<TResult, TNewResult>(this Task<TResult> task, Func<Task<TResult>, Task<TNewResult>> continuation)
            => task.ContinueWith(continuation).Unwrap();

        /// <summary>
        /// Creates a continuation that executes asynchronously when the target task completes.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TNewResult">The type of the new result.</typeparam>
        /// <param name="task">The target task.</param>
        /// <param name="continuation">The continuation function.</param>
        public static Task<TNewResult> Then<TResult, TNewResult>(this Task<TResult> task, Func<TResult, TNewResult> continuation)
            => task.ContinueWith(t => continuation(t.Result));

        /// <summary>
        /// Creates a continuation that executes asynchronously when the target task completes.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TNewResult">The type of the new result.</typeparam>
        /// <param name="task">The target task.</param>
        /// <param name="continuation">The continuation function.</param>
        public static Task<TNewResult> Then<TResult, TNewResult>(this Task<TResult> task, Func<TResult, Task<TNewResult>> continuation)
            => task.ContinueWith(t => continuation(t.Result)).Unwrap();

        /// <summary>
        /// Creates a continuation that executes asynchronously when the target task completes.
        /// </summary>
        /// <typeparam name="TNewResult">The type of the new result.</typeparam>
        /// <param name="task">The target task.</param>
        /// <param name="continuation">The continuation function.</param>
        public static Task<TNewResult> Then<TNewResult>(this Task task, Func<Task, TNewResult> continuation)
            => task.ContinueWith(continuation);

        /// <summary>
        /// Creates a continuation that executes asynchronously when the target task completes.
        /// </summary>
        /// <typeparam name="TNewResult">The type of the new result.</typeparam>
        /// <param name="task">The target task.</param>
        /// <param name="continuation">The continuation function.</param>
        public static Task<TNewResult> Then<TNewResult>(this Task task, Func<Task, Task<TNewResult>> continuation)
            => task.ContinueWith(continuation).Unwrap();

        /// <summary>
        /// Creates a continuation that executes asynchronously when the target task completes.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="task">The target task.</param>
        /// <param name="continuation">The continuation function.</param>
        public static Task Then<TResult>(this Task<TResult> task, Action<Task<TResult>> continuation)
            => task.ContinueWith(continuation);

        /// <summary>
        /// Creates a continuation that executes asynchronously when the target task completes.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="task">The target task.</param>
        /// <param name="continuation">The continuation function.</param>
        public static Task Then<TResult>(this Task<TResult> task, Action<TResult> continuation)
            => task.ContinueWith(t => continuation(t.Result));

        /// <summary>
        /// Creates a continuation that executes asynchronously when the target task completes.
        /// </summary>
        /// <param name="task">The target task.</param>
        /// <param name="continuation">The continuation function.</param>
        public static Task Then(this Task task, Action<Task> continuation)
            => task.ContinueWith(continuation);
    }
}