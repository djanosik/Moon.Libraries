using System;
using System.Collections.Generic;
using Xunit;

namespace Moon.Collections.Tests
{
    public class ListExtensionsTests
    {
        [Fact]
        public void Move_ShouldMoveItemToTheNewPosition()
        {
            var items = new List<int> { 1, 2, 3 };
            items.Move(0, 2);

            Assert.Equal(1, items[2]);
        }

        [Fact]
        public void RemoveDuplicates_EqualityComparer_RemovesDuplicates()
        {
            var items = new List<string> { "1", "2", "1", "3", "1", "4" };
            items.RemoveDuplicates(StringComparer.CurrentCulture);

            Assert.Equal(4, items.Count);
        }

        [Fact]
        public void RemoveDuplicates_Comparison_RemovesDuplicates()
        {
            var items = new List<string> { "1", "2", "1", "3", "1", "4" };
            items.RemoveDuplicates((Comparison<string>)string.Compare);

            Assert.Equal(4, items.Count);
        }

        [Fact]
        public void RemoveDuplicates_Comparer_RemovesDuplicates()
        {
            var items = new List<string> { "1", "2", "1", "3", "1", "4" };
            items.RemoveDuplicates((x, y) => x == y);

            Assert.Equal(4, items.Count);
        }
    }
}