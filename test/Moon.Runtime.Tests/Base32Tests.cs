using FluentAssertions;
using Xunit;

namespace Moon.Tests
{
    public class Base32Tests
    {
        [Fact]
        public void Encoding()
        {
            var input = new byte[] { 101, 105, 88, 94 };
            var encoded = Base32.Encode(input);
            var result = Base32.Decode(encoded);

            result.Should().BeEquivalentTo(input);
        }
    }
}