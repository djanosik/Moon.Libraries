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
    }
}