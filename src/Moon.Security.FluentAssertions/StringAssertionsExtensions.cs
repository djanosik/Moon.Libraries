using FluentAssertions;
using FluentAssertions.Primitives;

namespace Moon.Security
{
    public static class StringAssertionsExtensions
    {
        /// <summary>
        /// Asserts that a string is a hash of the given password.
        /// </summary>
        /// <param name="should">The string assertions.</param>
        /// <param name="password">The password to check the hash for.</param>
        public static AndConstraint<StringAssertions> BePBKDF2Of(this StringAssertions should, string password)
            => PBKDF2.Verify(should.Subject, password) ? should.Be(should.Subject) : should.NotBe(should.Subject);

        /// <summary>
        /// Asserts that a string is a hash of the given password.
        /// </summary>
        /// <param name="should">The string assertions.</param>
        /// <param name="password">The password to check the hash for.</param>
        public static AndConstraint<StringAssertions> BeBCryptOf(this StringAssertions should, string password)
            => BCrypt.Verify(should.Subject, password) ? should.Be(should.Subject) : should.NotBe(should.Subject);
    }
}