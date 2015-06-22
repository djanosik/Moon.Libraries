using System.Collections.Generic;
using Xunit;

namespace Moon.Reflection.Tests
{
    public class ClassTests
    {
        [Fact]
        public void Create_ReturnsNewInstance()
        {
            var list = Class.Create<List<int>>();

            Assert.IsAssignableFrom<List<int>>(list);
        }

        [Fact]
        public void Create_WithParams_ReturnsNewInstance()
        {
            var items = new[] { 1, 2, 3 };
            var list = Class.Create<List<int>>(items);

            Assert.IsAssignableFrom<List<int>>(list);
            Assert.Equal(3, list.Count);
        }
    }
}