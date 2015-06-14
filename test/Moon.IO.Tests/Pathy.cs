using Xunit;

namespace Moon.IO
{
    public class PathyTests
    {
        [Fact]
        public void AppendTrailingSlash_ShouldAppendSlash()
        {
            var result = Pathy.AppendTrailingSlash("/test/result");

            Assert.Equal("/test/result/", result);
        }

        [Fact]
        public void AppendTrailingSlash_EmptyString_ShouldReturnSlash()
        {
            var result = Pathy.AppendTrailingSlash("");

            Assert.Equal("/", result);
        }

        [Fact]
        public void IsAbsolutePath_AbsoluteFilePath_ShouldReturnTrue()
        {
            var result = Pathy.IsAbsolutePath("c:/test.txt");

            Assert.True(result);
        }

        [Fact]
        public void IsAbsolutePath_AbsoluteUrl_ShouldReturnTrue()
        {
            var result = Pathy.IsAbsolutePath("http://test.cz/");

            Assert.True(result);
        }

        [Theory]
        [InlineData("../test.txt"), InlineData("..\\test.txt")]
        [InlineData("/test.txt"), InlineData("test.txt")]
        public void IsAbsolutePath_RelativePath_ShouldReturnFalse(string path)
        {
            var result = Pathy.IsAbsolutePath(path);

            Assert.False(result);
        }

        [Fact]
        public void MakeAbsolute_AbsoluteFilePathAndRelativeInputPath_ShouldReturnInputConvertedToAbsolutePath()
        {
            const string input = "../test.txt";
            var result = Pathy.MakeAbsolute("c:/test/test.txt", input);

            Assert.Equal("c:/test.txt", result);
        }

        [Fact]
        public void MakeAbsolute_AbsoluteFolderPathAndRelativeInputPath_ShouldReturnInputConvertedToAbsolutePath()
        {
            const string input = "test.txt";
            var result = Pathy.MakeAbsolute("c:/test", input);

            Assert.Equal("c:/test.txt", result);
        }

        [Fact]
        public void MakeAbsolute_AbsoluteInputPath_ShouldReturnInput()
        {
            const string input = "c:/test.txt";
            var result = Pathy.MakeAbsolute("c:/anyabsolute.txt", input);

            Assert.Equal(input, result);
        }

        [Fact]
        public void MakeRelativeToRoot_ShouldReturnInputConvertedToRelativePath()
        {
            const string input = "c:/test/neco/test.txt";
            var result = Pathy.MakeRelativeToRoot("c:/test/", input);

            Assert.Equal("/neco/test.txt", result);
        }

        [Fact]
        public void Normalize_AbsolutePath_ShouldReturnNormalizedPath()
        {
            var result = Pathy.Normalize("c:\\test\\\\test.txt");

            Assert.Equal("c:/test/test.txt", result);
        }

        [Fact]
        public void Normalize_HttpUrl_ShouldReturnNormalizedPath()
        {
            var result = Pathy.Normalize("http:\\\\www.web.cz\\");

            Assert.Equal("http://www.web.cz/", result);
        }

        [Fact]
        public void Normalize_RelativePath_ShouldReturnNormalizedPath()
        {
            var result = Pathy.Normalize("..\\\\test.txt");

            Assert.Equal("../test.txt", result);
        }

        [Fact]
        public void RemoveLeadingSlash_ShouldReturnExpectedResult()
        {
            var result = Pathy.RemoveLeadingSlash("/test/result");

            Assert.Equal("test/result", result);
        }

        [Fact]
        public void RemoveLeadingSlash_EmptyString_ShouldReturnExpectedResult()
        {
            var result = Pathy.RemoveLeadingSlash("");

            Assert.Equal("", result);
        }

        [Fact]
        public void RemoveTrailingSlash_ShouldReturnExpectedResult()
        {
            var result = Pathy.RemoveTrailingSlash("/test/result/");

            Assert.Equal("/test/result", result);
        }

        [Fact]
        public void RemoveTrailingSlash_EmptyString_ShouldReturnExpectedResult()
        {
            var result = Pathy.RemoveTrailingSlash("");

            Assert.Equal("", result);
        }
    }
}