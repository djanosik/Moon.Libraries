using FluentAssertions;
using Xunit;

namespace Moon.Tests
{
    public class MimeTypeTests
    {
        [Fact]
        public void GettingTypeOfKnownExtension()
        {
            var result = MimeType.Get(".json");
            result.Should().Be("application/json");
        }

        [Fact]
        public void GettingTypeOfKnownFileName()
        {
            var result = MimeType.Get("data.json");
            result.Should().Be("application/json");
        }

        [Fact]
        public void GettingTypeOfUnknownExtension()
        {
            var result = MimeType.Get(".unk");
            result.Should().Be("application/octet-stream");
        }
    }
}