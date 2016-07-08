using FluentAssertions;
using Xunit;

namespace Moon.Collections.Tests
{
    public class ArrayExtensionsTests
    {
        [Fact]
        public void MovingFirstItemToLastPosition()
        {
            var numbers = new[] { 1, 2, 3 };
            numbers.Move(0, 2);

            numbers[2].Should().Be(1);
        }

        [Fact]
        public void TrimmingArray()
        {
            var numbers = new[] { 1, 2, 3 };
            var result = numbers.Trim(1);

            result.Should().HaveCount(1);
            result[0].Should().Be(numbers[0]);
        }
    }
}