using FluentAssertions;
using Xunit;

namespace Moon.Reflection.Tests
{
    public class AnonymousObjectTests
    {
        [Fact]
        public void ConvertingAnonymousObjectToDictionary()
        {
            var anonymous = new { test = "test", test2 = "test2" };
            var result = AnonymousObject.ToDictionary(anonymous);

            result["test"].Should().Be("test");
            result["test2"].Should().Be("test2");
        }
    }
}