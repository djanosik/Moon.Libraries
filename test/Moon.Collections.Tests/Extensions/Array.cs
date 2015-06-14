using System.Linq;
using Xunit;

namespace Moon.Collections.Tests
{
    public class ArrayExtensionsTests
    {
        [Fact]
        public void Move_ShouldMoveItemToTheNewPosition()
        {
            var items = new[] { 1, 2, 3 };
            items.Move(0, 2);

            Assert.Equal(1, items[2]);
        }

        [Fact]
        public void Trim_SholdReturnTrimmedArray()
        {
            var result = new[] { 1, 2, 3 }.Trim(1);

            Assert.Equal(1, result.Last());
        }
    }
}