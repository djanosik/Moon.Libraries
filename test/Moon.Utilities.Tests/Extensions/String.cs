using System;
using Xunit;

namespace Moon.Utilities.Tests
{
	public class StringExtensionsTests
	{
        [Fact]
        public void AsUrlKey_AnyString_ShoulTrimWhiteSpace()
        {
            var result = " text ".AsUrlKey();

            Assert.Equal("text", result);
        }        
        
        [Fact]
        public void AsUrlKey_AnyString_ShouldRemoveDiacritics()
        {
            var result = "ěščěřščžčřžňť".AsUrlKey();

            Assert.Equal("escersczcrznt", result);
        }

        [Fact]
        public void AsUrlKey_AnyString_ShouldReplaceWhiteSpaceWithHyphen()
        {
            var result = "text     text".AsUrlKey();

            Assert.Equal("text-text", result);
        }

        [Fact]
        public void Capitalize_AnyString_ShouldConvertFirstLetterToUpperCase()
        {
            var result = "test text".Capitalize();

            Assert.Equal("Test text", result);
        }

        [Fact]
        public void RemoveDiacritics_AnyString_ShouldRemoveDiacritics()
        {
            var result = "ěščřžýáíéťň".RemoveDiacritics();

            Assert.Equal("escrzyaietn", result);
        }

        [Fact]
        public void RemoveAll_AnyString_ShouldRemoveAllOccurencesOfString()
        {
            var result = "testTextOfText".RemoveAll("Text");

            Assert.Equal("testOf", result);
        }

        [Fact]
        public void RemoveFirst_AnyString_ShouldRemoveFirstOccurenceOfString()
        {
            var result = "testTextOfText".RemoveFirst("Text");

            Assert.Equal("testOfText", result);
        }

        [Fact]
        public void RemoveLast_AnyString_ShouldRemoveLastOccurenceOfString()
        {
            var result = "testTextOfText".RemoveLast("Text");

            Assert.Equal("testTextOf", result);
        }
        
        [Fact]
        public void Replace_AnyString_ShouldReplaceStringUsingTheGivenComparisonMethod()
        {
            var result = "text Text TeXt".Replace("text", "test", StringComparison.CurrentCultureIgnoreCase);

            Assert.Equal("test test test", result);
        }

        [Fact]
        public void Shorten_AnyString_ShouldReturnShortenedString()
        {
            const string input = "abcdefghijklmnopqrstuvwxyz";
            string result = input.Shorten(9);

            Assert.True(result.Length < input.Length);
        }
	}	
}