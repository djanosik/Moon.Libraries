using FluentAssertions;
using Moon.Security;
using Xbehave;

namespace Moon.Tests
{
    public class PasswordHashTests
    {
        string password, hash;
        bool result;

        [Scenario]
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