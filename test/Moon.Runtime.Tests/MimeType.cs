using FluentAssertions;
using Moon.Testing;
using Xunit;

namespace Moon.Tests
{
    public class MimeTypeTests
    {
        string input, result;

        [Fact]
        public void GettingTypeOfKnownFileName()
        {
            "Given the file name"
                .x(() => input = "data.json");

            "When I get MIME type for the file name"
                .x(() => result = MimeType.Get(input));

            "Then it should be 'application/json'"
                .x(() =>
                {
                    result.Should().Be("application/json");
                });
        }

        [Fact]
        public void GettingTypeOfKnownExtension()
        {
            "Given the file extension"
                .x(() => input = ".json");

            "When I get MIME type for the file extension"
                .x(() => result = MimeType.Get(input));

            "Then it should be 'application/json'"
                .x(() =>
                {
                    result.Should().Be("application/json");
                });
        }

        [Fact]
        public void GettingTypeOfUnknownExtension()
        {
            "Given the file extension"
                .x(() => input = ".unk");

            "When I get MIME type for the file extension"
                .x(() => result = MimeType.Get(input));

            "Then it should be 'application/octet-stream'"
                .x(() =>
                {
                    result.Should().Be("application/octet-stream");
                });
        }
    }
}