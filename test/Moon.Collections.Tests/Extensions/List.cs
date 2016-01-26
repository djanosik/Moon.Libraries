using System;
using System.Collections.Generic;
using FluentAssertions;
using Moon.Testing;
using Xunit;

namespace Moon.Collections.Tests
{
    public class ListExtensionsTests : TestSetup
    {
        List<int> numbers;
        List<string> strings;

        [Fact]
        public void MovingItem()
        {
            "Given the list"
                .x(() => numbers = new List<int> { 1, 2, 3 });

            "When I move first item to last position"
                .x(() => numbers.Move(0, 2));

            "Then the last item should be 1"
                .x(() =>
                {
                    numbers[2].Should().Be(1);
                });
        }

        [Fact]
        public void RemovingDuplicatesUsingEqualityComparer()
        {
            "Given the list"
                .x(() => strings = new List<string> { "1", "2", "1", "3", "1", "4" });

            "When I remove duplicates using equality comparer"
                .x(() => strings.RemoveDuplicates(StringComparer.CurrentCulture));

            "Then it should contain 4 items"
                .x(() => strings.Should().HaveCount(4));
        }

        [Fact]
        public void RemovingDuplicatesUsingComparisonDelegate()
        {
            "Given the list"
                .x(() => strings = new List<string> { "1", "2", "1", "3", "1", "4" });

            "When I remove duplicates using comparison delegate"
                .x(() => strings.RemoveDuplicates((Comparison<string>)string.Compare));

            "Then it should contain 4 items"
                .x(() => strings.Should().HaveCount(4));
        }

        [Fact]
        public void RemovingDuplicatesUsingComparerLambda()
        {
            "Given the list"
                .x(() => strings = new List<string> { "1", "2", "1", "3", "1", "4" });

            "When I remove duplicates using comparer lambda"
                .x(() => strings.RemoveDuplicates((x, y) => x == y));

            "Then it should contain 4 items"
                .x(() => strings.Should().HaveCount(4));
        }
    }
}