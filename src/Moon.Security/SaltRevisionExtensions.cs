using System;

namespace Moon.Security
{
    public static class SaltRevisionExtensions
    {
        /// <summary>
        /// Returns a string representation of the salt revision.
        /// </summary>
        /// <param name="revision">The salt revision.</param>
        public static string ToRevisionString(this SaltRevision revision)
        {
            switch (revision)
            {
                case SaltRevision.Revision2:
                    return "2";
                case SaltRevision.Revision2A:
                    return "2a";
                case SaltRevision.Revision2B:
                    return "2b";
                case SaltRevision.Revision2X:
                    return "2x";
                case SaltRevision.Revision2Y:
                    return "2y";
                default:
                    throw new InvalidOperationException($"The revision '{revision}' is not supported!");
            }
        }
    }
}