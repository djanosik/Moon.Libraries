using FluentAssertions;
using Xunit;

namespace Moon.Security.Tests
{
    public class TOTPTests
    {
        [Fact]
        public void Veryfying()
        {
            var key = TOTP.GenerateKey();
            var code = TOTP.GenerateCode(key);
            var result = TOTP.VerifyCode(key, code);

            result.Should().BeTrue();
        }
    }
}