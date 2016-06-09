using FluentAssertions;
using Xbehave;

namespace Moon.Collections.Tests
{
    public class ArrayExtensionsTests
    {
        int[] numbers, result;

        [Scenario]
        public void MovingItem()
        {
            "Given the array"
                .x(() => numbers = new[] { 1, 2, 3 });

            "When I move first item to last position"
                .x(() => numbers.Move(0, 2));

            "Then the last item should be 1"
                .x(() =>
                {
                    numbers[2].Should().Be(1);
                });
        }

        [Scenario]
        public void TrimmingArray()
        {
            "Given the array"
                .x(() => numbers = new[] { 1, 2, 3 });

            "When I trim the array and get the result"
                .x(() => result = numbers.Trim(1));

            "Then it should return an array with the first item"
                .x(() =>
                {
                    result.Should().HaveCount(1);
                    result[0].Should().Be(numbers[0]);
                });
        }
    }
}