using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Moon.Globalization
{
    /// <summary>
    /// Provides helpers for dealing with currencies.
    /// </summary>
    public static class Currency
    {
        private static Dictionary<string, string> cultureMap;
        private static string[] negativePatterns;
        private static string[] positivePatterns;

        /// <summary>
        /// Initializes the <see cref="Currency" /> class.
        /// </summary>
        static Currency()
        {
            negativePatterns = new[] { "($n)", "-$n", "$-n", "$n-", "(n$)",
                "-n$", "n-$", "n$-", "-n $", "-$ n", "n $-", "$ n-", "$ -n",
                "n- $", "($ n)", "(n $)" };

            positivePatterns = new[] { "$n", "n$", "$ n", "n $" };

            cultureMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["AUD"] = "en-AU",
                ["BGN"] = "bg-BG",
                ["CAD"] = "en-CA",
                ["CNY"] = "zh-CN",
                ["CZK"] = "cs-CZ",
                ["DKK"] = "da-DK",
                ["EUR"] = "fr-FR",
                ["GBP"] = "en-GB",
                ["HRK"] = "hr-HR",
                ["HUF"] = "hu-HU",
                ["INR"] = "hi-IN",
                ["JPY"] = "ja-JP",
                ["KWD"] = "ar-KW",
                ["NOK"] = "nn-NO",
                ["NZD"] = "en-NZ",
                ["PLN"] = "pl-PL",
                ["RUB"] = "ru-RU",
                ["SEK"] = "sv-SE",
                ["THB"] = "th-TH",
                ["TRY"] = "tr-TR",
                ["UAH"] = "uk-UA",
                ["USD"] = "en-US"
            };
        }

        /// <summary>
        /// Returns a composite string used to format the specified currency code. Only limited
        /// subset of currencies is supported.
        /// </summary>
        /// <param name="currencyCode">
        /// A three-letter currency code (see http://en.wikipedia.org/wiki/ISO_4217) :).
        /// </param>
        public static string GetFormat(string currencyCode)
        {
            Requires.NotNull(currencyCode, nameof(currencyCode));

            if (!cultureMap.ContainsKey(currencyCode))
            {
                return "{{0}} {0}".With(currencyCode.ToUpper());
            }

            var culture = new CultureInfo(cultureMap[currencyCode]);
            var numberFormat = culture.NumberFormat;

            var result = "{{0:{0};{1}}}".With(positivePatterns[numberFormat.CurrencyPositivePattern],
                negativePatterns[numberFormat.CurrencyNegativePattern]);

            result = result.Replace("n", GetPattern(numberFormat));
            result = result.Replace("$", $"'{numberFormat.CurrencySymbol}'");
            result = result.Replace("-", $"'{numberFormat.NegativeSign}'");

            return result;
        }

        /// <summary>
        /// Returns currency symbol corresponding to the specified currency code. Only limited
        /// subset of currency symbols is supported.
        /// </summary>
        /// <param name="currencyCode">
        /// A three-letter currency code (see http://en.wikipedia.org/wiki/ISO_4217) :).
        /// </param>
        public static string GetSymbol(string currencyCode)
        {
            Requires.NotNull(currencyCode, nameof(currencyCode));

            if (!cultureMap.ContainsKey(currencyCode))
            {
                return currencyCode.ToUpper();
            }

            var culture = new CultureInfo(cultureMap[currencyCode]);
            return culture.NumberFormat.CurrencySymbol;
        }

        private static string GetPattern(NumberFormatInfo numberFormat)
        {
            var builder = new StringBuilder("#,0.", 10);

            for (var i = 1; i < numberFormat.CurrencyDecimalDigits; i++)
            {
                builder.Append("0");
            }

            return builder.ToString();
        }
    }
}