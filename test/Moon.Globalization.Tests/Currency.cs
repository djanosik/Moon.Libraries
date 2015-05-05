using Xunit;

namespace Moon.Globalization.Tests
{
    public class CurrencyTests
    {
        [Theory]
        [InlineData("EUR", "€"), InlineData("CZK", "Kč"), InlineData("USD", "$")]
        [InlineData("JPY", "¥"), InlineData("GBP", "£"), InlineData("CNY", "¥")]
        public void GetSymbol_ShouldReturnExpectedResult(string currency, string expected)
        {
            var result = Currency.GetSymbol(currency);

            Assert.Equal(expected, result);
        }
    }
}