using FluentAssertions;
using Xunit;

namespace Moon.IO.Tests
{
    public class PathyTests
    {
        [Theory]
        [InlineData("../test.txt"), InlineData("..\\test.txt")]
        [InlineData("/test.txt"), InlineData("test.txt")]
        public void CheckingWhetherRelativePathIsAbsolute(string relative)
        {
            var result = Pathy.IsAbsolutePath(relative);
            result.Should().BeFalse();
        }

        [Fact]
        public void AppendingTrailingSlash()
        {
            var result = Pathy.AppendTrailingSlash("/test/result");
            result.Should().Be("/test/result/");
        }

        [Fact]
        public void AppendingTrailingSlashToEmptyString()
        {
            var result = Pathy.AppendTrailingSlash(string.Empty);
            result.Should().Be("/");
        }

        [Fact]
        public void CheckingWhetherAbsolutePathIsAbsolute()
        {
            var result = Pathy.IsAbsolutePath("c:/test.txt");
            result.Should().BeTrue();
        }

        [Fact]
        public void CheckingWhetherAbsoluteUrlIsAbsolute()
        {
            var result = Pathy.IsAbsolutePath("http://test.cz/");
            result.Should().BeTrue();
        }

        [Fact]
        public void MakingAbsolutePathAbsolute()
        {
            var result = Pathy.MakeAbsolute("c:/anyabsolute.txt", "c:/test.txt");
            result.Should().Be("c:/test.txt");
        }

        [Fact]
        public void MakingAbsolutePathRelativeToRoot()
        {
            var result = Pathy.MakeRelativeToRoot("c:/test/", "c:/test/neco/test.txt");
            result.Should().Be("/neco/test.txt");
        }

        [Fact]
        public void MakingParentRelativePathAbsolute()
        {
            var result = Pathy.MakeAbsolute("c:/test/test.txt", "../test.txt");
            result.Should().Be("c:/test.txt");
        }

        [Fact]
        public void MakingRelativePathAbsolute()
        {
            var result = Pathy.MakeAbsolute("c:/test", "test.txt");
            result.Should().Be("c:/test.txt");
        }

        [Fact]
        public void NormalizingAbsolutePath()
        {
            var result = Pathy.Normalize("c:\\test\\\\test.txt");
            result.Should().Be("c:/test/test.txt");
        }

        [Fact]
        public void NormalizingAbsoluteUrl()
        {
            var result = Pathy.Normalize("http:\\\\www.web.cz\\");
            result.Should().Be("http://www.web.cz/");
        }

        [Fact]
        public void NormalizingRelativePath()
        {
            var result = Pathy.Normalize("..\\\\test.txt");
            result.Should().Be("../test.txt");
        }

        [Fact]
        public void RemovingLeadingSlash()
        {
            var result = Pathy.RemoveLeadingSlash("/test/result");
            result.Should().Be("test/result");
        }

        [Fact]
        public void RemovingLeadingSlashFromEmptyString()
        {
            var result = Pathy.RemoveLeadingSlash(string.Empty);
            result.Should().Be(string.Empty);
        }

        [Fact]
        public void RemovingTrailingSlash()
        {
            var result = Pathy.RemoveTrailingSlash("/test/result/");
            result.Should().Be("/test/result");
        }

        [Fact]
        public void RemovingTrailingSlashFromEmptyString()
        {
            var result = Pathy.RemoveTrailingSlash(string.Empty);
            result.Should().Be(string.Empty);
        }
    }
}