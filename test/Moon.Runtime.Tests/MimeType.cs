using Xunit;

namespace Moon.Tests
{
    public class MimeTypeTests
    {
        [Fact]
        public void Get_FileWithKnownExtension_ShouldReturnCorrectMimeType()
        {
            var result = MimeType.Get("data.json");

            Assert.Equal(result, "application/json");
        }

        [Fact]
        public void Get_KnownExtension_ShouldReturnCorrectMimeType()
        {
            var result = MimeType.Get(".json");

            Assert.Equal(result, "application/json");
        }

        [Fact]
        public void Get_UnknownExtension_ShouldReturnDefaultMimeType()
        {
            var result = MimeType.Get(".test");

            Assert.Equal(result, "application/octet-stream");
        }
    }
}