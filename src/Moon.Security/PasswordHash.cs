using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Moon.Security
{
    /// <summary>
    /// Utility used to compute and verify password hashes. The hash is computed by using PBKDF2
    /// with HMAC-SHA256, 128-bit salt, 256-bit subkey and 10000 iteration.
    /// </summary>
    public static class PasswordHash
    {
        private const int saltSize = 128 / 8;
        private const int subkeySize = 256 / 8;

        private static readonly RandomNumberGenerator random;

        /// <summary>
        /// Initializes the <see cref="PasswordHash" /> class.
        /// </summary>
        static PasswordHash()
        {
            random = RandomNumberGenerator.Create();
        }

        /// <summary>
        /// Returns a hashed representation of the supplied <paramref name="password" />.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        public static string Hash(string password)
        {
            Requires.NotNull(password, nameof(password));

            var hash = Hash(password, KeyDerivationPrf.HMACSHA256, 10000);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Returns a value indicating whether the given hash is valid for the specified password.
        /// </summary>
        /// <param name="hash">The hashed representation of password.</param>
        /// <param name="password">The password supplied for comparison.</param>
        public static bool Verify(string hash, string password)
        {
            Requires.NotNull(hash, nameof(hash));
            Requires.NotNull(password, nameof(password));

            var decodedHash = Convert.FromBase64String(hash);

            if (decodedHash.Length != 0)
            {
                return Verify(decodedHash, password);
            }

            return false;
        }

        private static byte[] Hash(string password, KeyDerivationPrf prf, int iterationCount)
        {
            var salt = new byte[saltSize];
            random.GetBytes(salt);

            var subkey = KeyDerivation.Pbkdf2(password, salt, prf, iterationCount, subkeySize);

            var result = new byte[12 + salt.Length + subkey.Length];
            WriteNetworkByteOrder(result, 0, (uint)prf);
            WriteNetworkByteOrder(result, 4, (uint)iterationCount);
            WriteNetworkByteOrder(result, 8, saltSize);
            Buffer.BlockCopy(salt, 0, result, 12, salt.Length);
            Buffer.BlockCopy(subkey, 0, result, 12 + saltSize, subkey.Length);
            return result;
        }

        private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
        {
            buffer[offset + 0] = (byte)(value >> 24);
            buffer[offset + 1] = (byte)(value >> 16);
            buffer[offset + 2] = (byte)(value >> 8);
            buffer[offset + 3] = (byte)(value >> 0);
        }

        private static bool Verify(byte[] hash, string password)
        {
            try
            {
                var prf = (KeyDerivationPrf)ReadNetworkByteOrder(hash, 0);
                var iterationCount = (int)ReadNetworkByteOrder(hash, 4);
                var saltLength = (int)ReadNetworkByteOrder(hash, 8);

                if (saltLength < saltSize)
                {
                    return false;
                }

                var salt = new byte[saltLength];
                Buffer.BlockCopy(hash, 12, salt, 0, salt.Length);

                var subkeyLength = hash.Length - 12 - salt.Length;

                if (subkeyLength < saltSize)
                {
                    return false;
                }

                var expectedSubkey = new byte[subkeyLength];
                Buffer.BlockCopy(hash, 12 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

                var actualSubkey = KeyDerivation.Pbkdf2(password, salt, prf, iterationCount, subkeyLength);
                return ByteArraysEqual(actualSubkey, expectedSubkey);
            }
            catch
            {
                return false;
            }
        }

        private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            return ((uint)buffer[offset + 0] << 24)
                   | ((uint)buffer[offset + 1] << 16)
                   | ((uint)buffer[offset + 2] << 8)
                   | buffer[offset + 3];
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if ((a == null) && (b == null))
            {
                return true;
            }
            if ((a == null) || (b == null) || (a.Length != b.Length))
            {
                return false;
            }
            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= a[i] == b[i];
            }
            return areSame;
        }
    }
}