using System.Collections.Generic;
using System.Linq;
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
            var list = Class.Create<List<int>>(1);

            Assert.IsAssignableFrom<List<int>>(list);
            Assert.Equal(1, list.Capacity);
        }
    }
}