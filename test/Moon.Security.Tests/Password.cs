using FluentAssertions;
using Moon.Testing;
using Xunit;

namespace Moon.Security.Tests
{
    public class PasswordTests : TestSetup
    {
        int length;
        string result;

        [Fact]
        public void GeneratingPassword()
        {
            "Given the length"
                .x(() => length = 10);

            "When I generate a password"
                .x(() => result = Password.Generate(length));

            "Then it should be 10 chars long"
                .x(() =>
                {
                    result.Should().HaveLength(length);
                });
        }
    }
}