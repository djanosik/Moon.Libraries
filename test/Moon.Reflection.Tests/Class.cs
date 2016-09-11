using System.Collections.Generic;
using Xunit;

namespace Moon.Reflection.Tests
{
    public class ClassTests
    {
        [Fact]
        public void ActivatingInstance()
        {
            var list = Class.Activate<List<int>>();

            Assert.IsAssignableFrom<List<int>>(list);
        }

        [Fact]
        public void ActivatingInstanceWithParameters()
        {
            var list = Class.Activate<List<int>>(1);

            Assert.IsAssignableFrom<List<int>>(list);
            Assert.Equal(1, list.Capacity);
        }
    }
}