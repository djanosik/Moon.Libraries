using System.Linq;
using Xunit;

namespace Moon.Collections.Tests
{
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void FirstHalf_ShouldReturnFiveItems()
        {
            var source = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var results = source.FirstHalf().ToArray();

            Assert.Equal(5, results.Count());
            Assert.True(results.SequenceEqual(new[] { 1, 2, 3, 4, 5 }));
        }

        [Fact]
        public void FirstHalf_ShouldReturnThreeItems()
        {
            var source = new[] { 1, 2, 3, 4, 5, 6 };

            var results = source.FirstHalf().ToArray();

            Assert.Equal(3, results.Count());
            Assert.True(results.SequenceEqual(new[] { 1, 2, 3 }));
        }

        [Fact]
        public void SecondHalf_ShouldReturnFourItems()
        {
            var source = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var results = source.SecondHalf().ToArray();

            Assert.Equal(4, results.Count());
            Assert.True(results.SequenceEqual(new[] { 6, 7, 8, 9 }));
        }

        [Fact]
        public void SecondHalf_ShouldReturnThreeItems()
        {
            var source = new[] { 1, 2, 3, 4, 5, 6 };

            var results = source.SecondHalf().ToArray();

            Assert.Equal(3, results.Count());
            Assert.True(results.SequenceEqual(new[] { 4, 5, 6 }));
        }
    }
}