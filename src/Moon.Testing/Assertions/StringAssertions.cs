﻿using FluentAssertions;
using FluentAssertions.Primitives;
using Moon.Security;

namespace Moon.Testing
{
    /// <summary>
    /// <see cref="StringAssertions" /> extension methods.
    /// </summary>
    public static class StringAssertionsExtensions
    {
        /// <summary>
        /// Asserts that a string is a hash of the given password.
        /// </summary>
        /// <param name="should">The string assertions.</param>
        /// <param name="password">The password to check the hash for.</param>
        public static AndConstraint<StringAssertions> BeHashOf(this StringAssertions should, string password)
            => PasswordHash.Verify(should.Subject, password) ? should.Be(should.Subject) : should.NotBe(should.Subject);
    }
}