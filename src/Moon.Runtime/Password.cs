using System;

namespace Moon
{
    /// <summary>
    /// Utility for dealing with passwords.
    /// </summary>
    public static class Password
    {
        static readonly Random random = new Random();

        /// <summary>
        /// Generates a password of the specified length.
        /// </summary>
        /// <param name="length">The length of the password. It must be greater than 5.</param>
        public static string Generate(int length = 12)
        {
            Requires.That(length >= 5, "length is less than 5");

            var chars = new char[length];
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";

            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[random.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}