using FluentAssertions;
using Xunit;

namespace Moon.Reflection.Tests
{
    public class AnonymousTests
    {
        [Fact]
        public void ConvertingAnonymousToDictionary()
        {
            var anonymous = new { test = "test", test2 = "test2" };
            var result = Anonymous.ToDictionary(anonymous);

            result["test"].Should().Be("test");
            result["test2"].Should().Be("test2");
        }
    }
}