using FluentAssertions;
using Xunit;

namespace Moon.Security.Tests
{
    public class BCryptTests
    {
        [Fact]
        public void Hashing()
        {
            const string password = "ePXpPVR3oGSbJ1biQjD2";

            var hash = BCrypt.HashPassword(password);
            var result = BCrypt.Verify(hash, password);

            result.Should().BeTrue();
        }
    }
}