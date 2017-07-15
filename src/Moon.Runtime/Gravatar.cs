using System.Security.Cryptography;
using System.Text;

namespace Moon
{
    /// <summary>
    /// Utility for generating Gravatar URLs.
    /// </summary>
    public static class Gravatar
    {
        /// <summary>
        /// Returns a Gravatar URL for the given e-mail address.
        /// </summary>
        /// <param name="email">The e-mail address.</param>
        /// <param name="size">The size (width) of the avatar. </param>
        /// <param name="defaultImageUrl">The default image used when no avatar is associated with the <paramref name="email" />.</param>
        public static string GetUrl(string email, int? size = null, string defaultImageUrl = null)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            var builder = new StringBuilder();
            builder.Append($"https://www.gravatar.com/avatar/{ComputeHash(email)}");

            if (size != null)
            {
                builder.AppendQuery("s", size);
            }

            if (defaultImageUrl != null)
            {
                builder.AppendQuery("d", defaultImageUrl);
            }

            return builder.ToString();
        }

        private static string ComputeHash(string email)
        {
            email = email.ToLower().Trim();

            using (var md5 = MD5.Create())
            {
                var builder = new StringBuilder();
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(email));

                foreach (var byt in hash)
                {
                    builder.Append(byt.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}