using System;

namespace Moon.Security
{
    /// <summary>
    /// Exception thrown when there are issues with parsing BCrypt salt.
    /// </summary>
    public class SaltParseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="SaltParseException" />.
        /// </summary>
        /// <param name="message">The error message.</param>
        public SaltParseException(string message)
            : base(message)
        {
        }
    }
}