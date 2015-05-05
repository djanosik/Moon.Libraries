using Xunit;

namespace Moon.Tests
{
    public class PasswordTests
    {
        [Fact]
        public void GeneratePassword_ShouldReturnStringWithDefaultLength()
        {
            var result = Password.Generate();

            Assert.Equal(12, result.Length);
        }
    }
}