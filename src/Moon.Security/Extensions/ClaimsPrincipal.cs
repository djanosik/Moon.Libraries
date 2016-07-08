using System.Security.Claims;

namespace Moon.Security
{
    /// <summary>
    /// <see cref="ClaimsPrincipal" /> extension methods.
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Returns whether the user is authenticated.
        /// </summary>
        /// <param name="principal">The application user.</param>
        public static bool IsAuthenticated(this ClaimsPrincipal principal)
            => principal.Identity.IsAuthenticated;

        /// <summary>
        /// Returns the value for the first claim of the specified type.
        /// </summary>
        /// <param name="principal">The application user.</param>
        /// <param name="claimType">The claim type whose first value should be returned.</param>
        public static string FindFirstValue(this ClaimsPrincipal principal, string claimType)
            => principal.FindFirst(claimType)?.Value;
    }
}