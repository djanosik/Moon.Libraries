using Xunit;

namespace Moon.Reflection.Tests
{
    public class AnonymousTests
    {
        [Fact]
        public void ToDictionary_ShouldConvertPropertiesToItems()
        {
            var obj = new { test = "test", test2 = "test2" };
            var dictionary = Anonymous.ToDictionary<string>(obj);

            Assert.Equal(2, dictionary.Count);
        }
    }
}