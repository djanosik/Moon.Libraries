using System;
using System.Net;
using System.Security.Cryptography;

namespace Moon.Security
{
    /// <summary>
    /// The utility used to generated and verify TOTP codes.
    /// </summary>
    public static class TOTP
    {
        private static readonly RandomNumberGenerator random = RandomNumberGenerator.Create();
        private static readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly TimeSpan timeStep = TimeSpan.FromSeconds(30);

        /// <summary>
        /// Generates a pre-shared secret key for TOTP authenticators.
        /// </summary>
        public static string GenerateKey()
        {
            var key = new byte[20];
            random.GetBytes(key);
            return Base32.Encode(key);
        }

        /// <summary>
        /// Generates an URI to encode in QR code that could be scanned by TOTP authenticators.
        /// </summary>
        /// <param name="issuer">The identification of TOTP issuer.</param>
        /// <param name="email">The user's e-mail address.</param>
        /// <param name="key">The TOTP secret key.</param>
        public static string GenerateUri(string issuer, string email, string key)
        {
            Requires.NotNull(email, nameof(email));
            Requires.NotNull(key, nameof(key));

            issuer = WebUtility.UrlEncode(issuer);

            return $"otpauth://totp/{issuer}:{email}?secret={key}&issuer={issuer}";
        }

        /// <summary>
        /// Generates a TOTP verification code.
        /// </summary>
        /// <param name="key">The pre-shared secret key.</param>
        public static string GenerateCode(string key)
        {
            Requires.NotNull(key, nameof(key));

            var currentTimeStep = GetCurrentTimeStepNumber();
            using (var hashAlgorithm = new HMACSHA1(Base32.Decode(key)))
            {
                return ComputeTotp(hashAlgorithm, currentTimeStep)
                    .ToString();
            }
        }

        /// <summary>
        /// Returns whether the specified TOTP code is valid.
        /// </summary>
        /// <param name="key">The pre-shared secret key.</param>
        /// <param name="code">The TOTP code to validate.</param>
        public static bool VerifyCode(string key, string code)
        {
            Requires.NotNullOrEmpty(code, nameof(code));

            return key != null && int.TryParse(code, out int numeric) 
                && VerifyCode(Base32.Decode(key), numeric);
        }

        private static bool VerifyCode(byte[] key, int code)
        {
            var currentTimeStep = GetCurrentTimeStepNumber();

            using (var algorithm = new HMACSHA1(key))
            {
                for (var i = -2; i <= 2; i++)
                {
                    var computedTotp = ComputeTotp(algorithm, (ulong)((long)currentTimeStep + i));
                    if (computedTotp == code)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static int ComputeTotp(HashAlgorithm hashAlgorithm, ulong timeStepNumber)
        {
            var timeStepBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder((long)timeStepNumber));
            var hash = hashAlgorithm.ComputeHash(timeStepBytes);
            var offset = hash[hash.Length - 1] & 0xf;
            var binaryCode = (hash[offset] & 0x7f) << 24
                | (hash[offset + 1] & 0xff) << 16
                | (hash[offset + 2] & 0xff) << 8
                | hash[offset + 3] & 0xff;

            return binaryCode % 1000000;
        }

        private static ulong GetCurrentTimeStepNumber()
        {
            var delta = DateTime.UtcNow - unixEpoch;
            return (ulong)(delta.Ticks / timeStep.Ticks);
        }
    }
}