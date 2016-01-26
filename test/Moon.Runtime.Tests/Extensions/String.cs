using System;
using FluentAssertions;
using Moon.Testing;
using Xunit;

namespace Moon.Tests
{
    public class StringExtensionsTests
    {
        string text, result;
        StringComparison comparison;

        [Fact]
        public void CapitalizingText()
        {
            "Given the text"
                .x(() => text = "test text");

            "When I capitalize the text"
                .x(() => result = text.Capitalize());

            "Then it should return expected result"
                .x(() =>
                {
                    result.Should().Be("Test text");
                });
        }

        [Fact]
        public void RemovingDiacritics()
        {
            "Given the text"
                .x(() => text = "ěščřžýáíéťň");

            "When I remove diacritics from the text"
                .x(() => result = text.RemoveDiacritics());

            "Then it should return expected result"
                .x(() =>
                {
                    result.Should().Be("escrzyaietn");
                });
        }

        [Fact]
        public void RemovingAllOccurrences()
        {
            "Given the text"
                .x(() => text = "testTextOfText");

            "When I remove all occurrences of 'Text'"
                .x(() => result = text.RemoveAll("Text"));

            "Then it should return expected result"
                .x(() =>
                {
                    result.Should().Be("testOf");
                });
        }

        [Fact]
        public void RemovingFirstOccurrence()
        {
            "Given the text"
                .x(() => text = "testTextOfText");

            "When I remove first occurrence of 'Text'"
                .x(() => result = text.RemoveFirst("Text"));

            "Then it should return expected result"
                .x(() =>
                {
                    result.Should().Be("testOfText");
                });
        }

        [Fact]
        public void RemovingLastOccurrence()
        {
            "Given the text"
                .x(() => text = "testTextOfText");

            "When I remove last occurrence of 'Text'"
                .x(() => result = text.RemoveLast("Text"));

            "Then it should return expected result"
                .x(() =>
                {
                    result.Should().Be("testTextOf");
                });
        }

        [Fact]
        public void ReplacingOccurrences()
        {
            "Given the text"
                .x(() => text = "text Text TeXt");

            "And the string comparison"
                .x(() => comparison = StringComparison.CurrentCultureIgnoreCase);

            "When I replace 'text' occurrences with 'test'"
                .x(() => result = text.Replace("text", "test", comparison));

            "Then it should return expected result"
                .x(() =>
                {
                    result.Should().Be("test test test");
                });
        }
    }
}