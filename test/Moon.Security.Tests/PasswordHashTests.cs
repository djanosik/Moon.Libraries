using FluentAssertions;
using Xunit;

namespace Moon.Security.Tests
{
    public class PasswordHashTests
    {
        [Fact]
        public void Hashing()
        {
            const string password = "ePXpPVR3oGSbJ1biQjD2";

            var hash = PasswordHash.Hash(password);
            var result = PasswordHash.Verify(hash, password);

            result.Should().BeTrue();
        }
    }
}