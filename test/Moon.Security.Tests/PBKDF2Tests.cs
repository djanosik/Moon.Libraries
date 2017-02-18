using FluentAssertions;
using Xunit;

namespace Moon.Security.Tests
{
    public class PBKDF2Tests
    {
        [Fact]
        public void Hashing()
        {
            const string password = "ePXpPVR3oGSbJ1biQjD2";

            var hash = PBKDF2.HashPassword(password);
            var result = PBKDF2.Verify(hash, password);

            result.Should().BeTrue();
        }
    }
}