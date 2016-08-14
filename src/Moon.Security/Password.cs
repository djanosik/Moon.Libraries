using System;

namespace Moon.Security
{
    /// <summary>
    /// Utility for generating with passwords.
    /// </summary>
    public static class Password
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// Generates a 12 characters long password.
        /// </summary>
        public static string Generate()
            => Generate(12);

        /// <summary>
        /// Generates a password of the specified length.
        /// </summary>
        /// <param name="length">The length of the password..</param>
        public static string Generate(int length)
        {
            var chars = new char[length];
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";

            for (var i = 0; i < length; i++)
            {
                chars[i] = allowedChars[random.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}