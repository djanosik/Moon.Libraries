using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Moon.Collections.Tests
{
    public class ListExtensionsTests
    {
        [Fact]
        public void MovingFirstItemToLastPosition()
        {
            var numbers = new List<int> { 1, 2, 3 };
            numbers.Move(0, 2);

            numbers[2].Should().Be(1);
        }

        [Fact]
        public void RemovingDuplicatesUsingComparerLambda()
        {
            var strings = new List<string> { "1", "2", "1", "3", "1", "4" };
            strings.RemoveDuplicates((x, y) => x == y);

            strings.Should().HaveCount(4);
        }

        [Fact]
        public void RemovingDuplicatesUsingComparisonDelegate()
        {
            var strings = new List<string> { "1", "2", "1", "3", "1", "4" };
            strings.RemoveDuplicates((Comparison<string>)string.Compare);

            strings.Should().HaveCount(4);
        }

        [Fact]
        public void RemovingDuplicatesUsingEqualityComparer()
        {
            var strings = new List<string> { "1", "2", "1", "3", "1", "4" };
            strings.RemoveDuplicates(StringComparer.CurrentCulture);

            strings.Should().HaveCount(4);
        }
    }
}