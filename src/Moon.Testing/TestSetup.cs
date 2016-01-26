using System;
using System.Collections.Generic;
using System.Reflection;

namespace Moon.Testing
{
    /// <summary>
    /// The base class for test setups.
    /// </summary>
    public abstract class TestSetup : IDisposable
    {
        readonly IList<IDisposable> disposables;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestSetup" /> class.
        /// </summary>
        protected TestSetup()
        {
            disposables = new List<IDisposable>();
        }

        /// <summary>
        /// Gets the current assembly.
        /// </summary>
        public Assembly ThisAssembly
            => GetType().GetTypeInfo().Assembly;

        /// <summary>
        /// Cleans up resources used by the test.
        /// </summary>
        public void Dispose()
        {
            foreach (var disposable in disposables)
            {
                disposable.Dispose();
            }
        }

        /// <summary>
        /// Immediately registers the <see cref="IDisposable" /> object for disposal after all steps
        /// in the current scenario have been executed.
        /// </summary>
        /// <typeparam name="TDisposable">The type of the object.</typeparam>
        /// <param name="disposable">The object to register for disposal.</param>
        protected TDisposable Use<TDisposable>(TDisposable disposable)
            where TDisposable : class, IDisposable
        {
            if (disposable != null)
            {
                disposables.Add(disposable);
            }

            return disposable;
        }
    }
}