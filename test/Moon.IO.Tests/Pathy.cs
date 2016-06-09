using FluentAssertions;
using Xbehave;
using Xunit;

namespace Moon.IO
{
    public class PathyTests
    {
        string path, url;

        [Scenario]
        public void CheckingWhetherAbsolutePathIsAbsolute()
        {
            bool result = false;

            "Given the path"
                .x(() => path = "c:/test.txt");

            "When I check whether the path is absolute"
                .x(() => result = Pathy.IsAbsolutePath(path));

            "Then it should return true"
                .x(() =>
                {
                    result.Should().BeTrue();
                });
        }

        [Scenario]
        public void CheckingWhetherAbsoluteUrlIsAbsolute()
        {
            bool result = false;

            "Given the URL"
                .x(() => url = "http://test.cz/");

            "When I check whether the URL is absolute"
                .x(() => result = Pathy.IsAbsolutePath(url));

            "Then it should return true"
                .x(() =>
                {
                    result.Should().BeTrue();
                });
        }

        [Scenario]
        [InlineData("../test.txt"), InlineData("..\\test.txt")]
        [InlineData("/test.txt"), InlineData("test.txt")]
        public void CheckingWhetherRelativePathIsAbsolute(string relative)
        {
            bool result = false;

            "When I check whether the path is absolute"
                .x(() => result = Pathy.IsAbsolutePath(relative));

            "Then it should return false"
                .x(() =>
                {
                    result.Should().BeFalse();
                });
        }

        [Scenario]
        public void MakingParentRelativePathAbsolute()
        {
            string result = null;

            "Given the path"
                .x(() => path = "../test.txt");

            "When I make it absolute"
                .x(() => result = Pathy.MakeAbsolute("c:/test/test.txt", path));

            "Then the result should be absolute"
                .x(() =>
                {
                    result.Should().Be("c:/test.txt");
                });
        }

        [Scenario]
        public void MakingRelativePathAbsolute()
        {
            string result = null;

            "Given the path"
                .x(() => path = "test.txt");

            "When I make it absolute"
                .x(() => result = Pathy.MakeAbsolute("c:/test", path));

            "Then the result should be absolute"
                .x(() =>
                {
                    result.Should().Be("c:/test.txt");
                });
        }

        [Scenario]
        public void MakingAbsolutePathAbsolute()
        {
            string result = null;

            "Given the path"
                .x(() => path = "c:/test.txt");

            "When I make it absolute"
                .x(() => result = Pathy.MakeAbsolute("c:/anyabsolute.txt", path));

            "Then the result should be absolute"
                .x(() =>
                {
                    result.Should().Be(path);
                });
        }

        [Scenario]
        public void MakingAbsolutePathRelativeToRoot()
        {
            string result = null;

            "Given the path"
                .x(() => path = "c:/test/neco/test.txt");

            "When I make it relative to root"
                .x(() => result = Pathy.MakeRelativeToRoot("c:/test/", path));

            "Then the result should be relative"
                .x(() =>
                {
                    result.Should().Be("/neco/test.txt");
                });
        }

        [Scenario]
        public void NormalizingAbsolutePath()
        {
            string result = null;

            "Given the path"
                .x(() => path = "c:\\test\\\\test.txt");

            "When I normalize the path"
                .x(() => result = Pathy.Normalize(path));

            "Then the result should be normalized"
                .x(() =>
                {
                    result.Should().Be("c:/test/test.txt");
                });
        }

        [Scenario]
        public void NormalizingAbsoluteUrl()
        {
            string result = null;

            "Given the URL"
                .x(() => url = "http:\\\\www.web.cz\\");

            "When I normalize the URL"
                .x(() => result = Pathy.Normalize(url));

            "Then the result should be normalized"
                .x(() =>
                {
                    result.Should().Be("http://www.web.cz/");
                });
        }

        [Scenario]
        public void NormalizingRelativePath()
        {
            string result = null;

            "Given the path"
                .x(() => path = "..\\\\test.txt");

            "When I normalize the path"
                .x(() => result = Pathy.Normalize(path));

            "Then the result should be normalized"
                .x(() =>
                {
                    result.Should().Be("../test.txt");
                });
        }

        [Scenario]
        public void AppendingTrailingSlash()
        {
            string result = null;

            "Given the path"
                .x(() => path = "/test/result");

            "When I append trailing slash"
                .x(() => result = Pathy.AppendTrailingSlash(path));

            "Then the result should have trailing slash"
                .x(() =>
                {
                    result.Should().Be("/test/result/");
                });
        }

        [Scenario]
        public void AppendingTrailingSlashToEmptyString()
        {
            string result = null;

            "Given the path"
                .x(() => path = string.Empty);

            "When I append trailing slash"
                .x(() => result = Pathy.AppendTrailingSlash(path));

            "Then the result should have trailing slash"
                .x(() =>
                {
                    result.Should().Be("/");
                });
        }

        [Scenario]
        public void RemovingLeadingSlash()
        {
            string result = null;

            "Given the path"
                .x(() => path = "/test/result");

            "When I remove leading slash"
                .x(() => result = Pathy.RemoveLeadingSlash(path));

            "Then the result should not have leading slash"
                .x(() =>
                {
                    result.Should().Be("test/result");
                });
        }

        [Scenario]
        public void RemovingLeadingSlashFromEmptyString()
        {
            string result = null;

            "Given the path"
                .x(() => path = string.Empty);

            "When I remove leading slash"
                .x(() => result = Pathy.RemoveLeadingSlash(path));

            "Then the result should not have leading slash"
                .x(() =>
                {
                    result.Should().Be(path);
                });
        }

        [Scenario]
        public void RemovingTrailingSlash()
        {
            string result = null;

            "Given the path"
                .x(() => path = "/test/result/");

            "When I remove trailing slash"
                .x(() => result = Pathy.RemoveTrailingSlash(path));

            "Then the result should not have trailing slash"
                .x(() =>
                {
                    result.Should().Be("/test/result");
                });
        }

        [Scenario]
        public void RemovingTrailingSlashFromEmptyString()
        {
            string result = null;

            "Given the path"
                .x(() => path = string.Empty);

            "When I remove trailing slash"
                .x(() => result = Pathy.RemoveTrailingSlash(path));

            "Then the result should not have trailing slash"
                .x(() =>
                {
                    result.Should().Be(path);
                });
        }
    }
}