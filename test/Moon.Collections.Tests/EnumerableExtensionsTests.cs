using System.Linq;
using FluentAssertions;
using Xunit;

namespace Moon.Collections.Tests
{
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void GettingFirstHalfOfNineItems()
        {
            var source = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var result = source.FirstHalf().ToArray();

            result.Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5 });
        }

        [Fact]
        public void GettingFirstHalfOfSixItems()
        {
            var source = new[] { 1, 2, 3, 4, 5, 6 };
            var result = source.FirstHalf().ToArray();

            result.Should().BeEquivalentTo(new[] { 1, 2, 3 });
        }

        [Fact]
        public void GettingSecondHalfOfNineItems()
        {
            var source = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var result = source.SecondHalf().ToArray();

            result.Should().BeEquivalentTo(new[] { 6, 7, 8, 9 });
        }

        [Fact]
        public void GettingSecondHalfOfSixItems()
        {
            var source = new[] { 1, 2, 3, 4, 5, 6 };
            var result = source.SecondHalf().ToArray();

            result.Should().BeEquivalentTo(new[] { 4, 5, 6 });
        }
    }
}