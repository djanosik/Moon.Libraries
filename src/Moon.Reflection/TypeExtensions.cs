using System;
using System.Security.Cryptography;
using System.Text;

namespace Moon.Reflection
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Returns a Base64 encoded SHA-1 hash of type's full name.
        /// </summary>
        /// <param name="type">The <see cref="Type" /> instance.</param>
        public static string GetTypeHash(this Type type)
        {
            using (var sha1 = SHA1.Create())
            {
                var hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(type.FullName));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}