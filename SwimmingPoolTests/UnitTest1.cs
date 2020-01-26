using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using NUnit.Framework;
using System;
using System.Globalization;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        private const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA256;
        private const int Pbkdf2IterCount = 1000;
        private const int Pbkdf2SubkeyLength = 256 / 8;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var password = Environment.GetEnvironmentVariable("SwimmingPoolSystemUserPassword", EnvironmentVariableTarget.Machine);
            byte[] salt = ConvertHexStringToByteArray("0742b4969bebb3b9e5cf233a1bdfaf1c");
            var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength));
            Assert.Pass();
        }

        public static byte[] ConvertHexStringToByteArray(string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
            }

            byte[] data = new byte[hexString.Length / 2];
            for (int index = 0; index < data.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                data[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return data;
        }
    }
}