using System;
using FluentAssertions;
using Xunit;

namespace Moon.Tests
{
    public class StringExtensionsTests
    {
        [Fact]
        public void CapitalizingText()
        {
            var result = "test text".Capitalize();
            result.Should().Be("Test text");
        }

        [Fact]
        public void RemovingAllOccurrences()
        {
            var result = "testTextOfText".RemoveAll("Text");
            result.Should().Be("testOf");
        }

        [Fact]
        public void RemovingDiacritics()
        {
            var result = "ěščřžýáíéťň".RemoveDiacritics();
            result.Should().Be("escrzyaietn");
        }

        [Fact]
        public void RemovingFirstOccurrence()
        {
            var result = "testTextOfText".RemoveFirst("Text");
            result.Should().Be("testOfText");
        }

        [Fact]
        public void RemovingLastOccurrence()
        {
            var result = "testTextOfText".RemoveLast("Text");
            result.Should().Be("testTextOf");
        }

        [Fact]
        public void ReplacingOccurrences()
        {
            var result = "text Text TeXt".Replace("text", "test", StringComparison.CurrentCultureIgnoreCase);
            result.Should().Be("test test test");
        }
    }
}