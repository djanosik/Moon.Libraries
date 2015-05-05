using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Moon.Threading.Tasks
{
    /// <summary>
    /// Creates an instance of canceled <see cref="Task" />.
    /// </summary>
    public class CanceledTask : CanceledTask<bool>
    {
    }

    /// <summary>
    /// Creates an instance of canceled <see cref="Task{TResult}" />.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public class CanceledTask<TResult>
    {
        private readonly Task<TResult> task;

        /// <summary>
        /// Initializes a new instance of the <see cref="CanceledTask{TResult}" /> class.
        /// </summary>
        public CanceledTask()
        {
            var source = new TaskCompletionSource<TResult>();
            source.SetCanceled();
            task = source.Task;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="CanceledTask{TResult}" /> to <see cref="Task{TResult}" />.
        /// </summary>
        /// <param name="task">The canceled task.</param>
        public static implicit operator Task<TResult>(CanceledTask<TResult> task)
            => task;

        /// <summary>
        /// Performs an implicit conversion from <see cref="CanceledTask{TResult}" /> to <see cref="Task" />.
        /// </summary>
        /// <param name="task">The canceled task.</param>
        public static implicit operator Task(CanceledTask<TResult> task)
            => task;

        /// <summary>
        /// Gets an awaiter used to await this <see cref="CanceledTask{TResult}" />.
        /// </summary>
        public TaskAwaiter<TResult> GetAwaiter()
            => task.GetAwaiter();
    }
}