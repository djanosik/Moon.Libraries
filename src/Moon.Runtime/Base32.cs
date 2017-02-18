using System;
using System.Linq;

namespace Moon
{
    /// <summary>
    /// The utility to deal with Base32 encoding and decoding.
    /// </summary>
    public static class Base32
    {
        private const int encodedBitCount = 5;
        private const int byteBitCount = 8;
        private const string encodingChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
        private const char paddingCharacter = '=';

        /// <summary>
        /// Takes a block of data and converts it to a Base32 encoded string.
        /// </summary>
        /// <param name="data">The data to encode.</param>
        public static string Encode(byte[] data)
        {
            Requires.NotNull(data, nameof(data));
            Requires.That(data.Length > 0, "data is empty");

            // The output character count is calculated in 40 bit blocks. That is because the least
            // common blocks size for both binary (8 bit) and base 32 (5 bit) is 40. Padding must be
            // used to fill in the difference.

            var outputCharacterCount = (int)decimal.Ceiling(data.Length / (decimal)encodedBitCount) * byteBitCount;
            var outputBuffer = new char[outputCharacterCount];

            var workingValue = 0;
            var remainingBits = encodedBitCount;
            var currentPosition = 0;

            foreach (var workingByte in data)
            {
                workingValue = (byte)(workingValue | workingByte >> (byteBitCount - remainingBits));
                outputBuffer[currentPosition++] = encodingChars[workingValue];

                if (remainingBits <= byteBitCount - encodedBitCount)
                {
                    workingValue = (byte)(workingByte >> (byteBitCount - encodedBitCount - remainingBits) & 31);
                    outputBuffer[currentPosition++] = encodingChars[workingValue];
                    remainingBits += encodedBitCount;
                }

                remainingBits -= byteBitCount - encodedBitCount;
                workingValue = (byte)(workingByte << remainingBits & 31);
            }

            if (currentPosition != outputCharacterCount)
            {
                outputBuffer[currentPosition++] = encodingChars[workingValue];
            }

            // RFC 4648 specifies that padding up to the end of the next 40 bit block must be
            // provided Since the outputCharacterCount does account for the paddingCharacters, fill
            // it out.

            while (currentPosition < outputCharacterCount)
            {
                outputBuffer[currentPosition++] = paddingCharacter;
            }

            return new string(outputBuffer);
        }

        /// <summary>
        /// Takes a Base32 encoded value and converts it back to binary data.
        /// </summary>
        /// <param name="base32">The Base32 encoded string.</param>
        public static byte[] Decode(string base32)
        {
            Requires.NotNullOrEmpty(base32, nameof(base32));

            var unpaddedBase32 = base32.ToUpperInvariant().TrimEnd(paddingCharacter);

            if (unpaddedBase32.Any(c => encodingChars.IndexOf(c) < 0))
            {
                throw new Exception("Base32 string contains illegal characters.");
            }

            var outputByteCount = unpaddedBase32.Length * encodedBitCount / byteBitCount;
            var outputBuffer = new byte[outputByteCount];

            byte workingByte = 0;
            var bitsRemaining = byteBitCount;
            var arrayIndex = 0;

            foreach (var workingChar in unpaddedBase32)
            {
                int mask;
                var encodedCharacterNumericValue = encodingChars.IndexOf(workingChar);

                if (bitsRemaining > encodedBitCount)
                {
                    mask = encodedCharacterNumericValue << (bitsRemaining - encodedBitCount);
                    workingByte = (byte)(workingByte | mask);
                    bitsRemaining -= encodedBitCount;
                }
                else
                {
                    mask = encodedCharacterNumericValue >> (encodedBitCount - bitsRemaining);
                    workingByte = (byte)(workingByte | mask);
                    outputBuffer[arrayIndex++] = workingByte;
                    workingByte = (byte)(encodedCharacterNumericValue << (byteBitCount - encodedBitCount + bitsRemaining));
                    bitsRemaining += byteBitCount - encodedBitCount;
                }
            }

            return outputBuffer;
        }
    }
}