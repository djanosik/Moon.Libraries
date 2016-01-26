using System.Linq;
using FluentAssertions;
using Moon.Testing;
using Xunit;

namespace Moon.Collections.Tests
{
    public class EnumerableExtensionsTests : TestSetup
    {
        int[] source, result;

        [Fact]
        public void GettingFirstHalfOfNineItems()
        {
            "Given the source"
                .x(() => source = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            "When I get the first half"
                .x(() => result = source.FirstHalf().ToArray());

            "Then it should return 5 items"
                .x(() =>
                {
                    result.Should().BeEquivalentTo(new[] { 1, 2, 3, 4, 5 });
                });
        }

        [Fact]
        public void GettingSecondHalfOfNineItems()
        {
            "Given the source"
                .x(() => source = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

            "When I get the first half"
                .x(() => result = source.SecondHalf().ToArray());

            "Then it should return 4 items"
                .x(() =>
                {
                    result.Should().BeEquivalentTo(new[] { 6, 7, 8, 9 });
                });
        }

        [Fact]
        public void GettingFirstHalfOfSixItems()
        {
            "Given the source"
                .x(() => source = new[] { 1, 2, 3, 4, 5, 6 });

            "When I get the first half"
                .x(() => result = source.FirstHalf().ToArray());

            "Then it should return 3 items"
                .x(() =>
                {
                    result.Should().BeEquivalentTo(new[] { 1, 2, 3 });
                });
        }

        [Fact]
        public void GettingSecondHalfOfSixItems()
        {
            "Given the source"
                .x(() => source = new[] { 1, 2, 3, 4, 5, 6 });

            "When I get the first half"
                .x(() => result = source.SecondHalf().ToArray());

            "Then it should return 3 items"
                .x(() =>
                {
                    result.Should().BeEquivalentTo(new[] { 4, 5, 6 });
                });
        }
    }
}