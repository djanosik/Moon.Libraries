using FluentAssertions;
using Moon.Security;
using Moon.Testing;
using Xunit;

namespace Moon.Tests
{
    public class PasswordHashTests : TestSetup
    {
        string password, hash;
        bool result;

        [Fact]
        public void Hashing()
        {
            "Given the password"
                .x(() => password = "ePXpPVR3oGSbJ1biQjD2");

            "When I hash the password"
                .x(() => hash = PasswordHash.Hash(password));

            "And then verify the password using the hash"
                .x(() => result = PasswordHash.Verify(hash, password));

            "Then it should return true"
                .x(() =>
                {
                    result.Should().BeTrue();
                });
        }
    }
}