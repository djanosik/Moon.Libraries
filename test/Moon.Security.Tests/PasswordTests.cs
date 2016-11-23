using FluentAssertions;
using Xunit;

namespace Moon.Security.Tests
{
    public class PasswordTests
    {
        [Fact]
        public void GeneratingPassword()
        {
            const int length = 10;
            var result = Password.Generate(length);

            result.Should().HaveLength(length);
        }
    }
}