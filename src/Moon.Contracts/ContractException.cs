using System;

namespace Moon
{
    /// <summary>
    /// Exception raised when a contract is broken. Catch this exception type if you wish to
    /// differentiate between any Contract exception and other runtime exceptions.
    /// </summary>
    public class ContractException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContractException" /> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public ContractException(string message)
            : base(message)
        {
        }
    }
}