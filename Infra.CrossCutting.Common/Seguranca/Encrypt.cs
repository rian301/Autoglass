using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Autoglass.Infra.CrossCutting.Common.Seguranca
{
    public static class Encrypt
    {
        #region Methods
        public static string ObterMd5Hash(string input)
        {
            MD5 md5Hash = MD5.Create();
            input = string.Concat("FFD6028E522647AB9F4CE5444698847A", input);

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sbBuilder = new StringBuilder();

            foreach (byte t in data)
            {
                sbBuilder.Append(t.ToString("x2"));
            }

            return sbBuilder.ToString();
        }

        public static string GetHash(string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        public static string EncryptText(string plainText, int keySize)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes("!1A3g2D4s9K556g7");
            byte[] saltValueBytes = Encoding.ASCII.GetBytes("KezzPassPhaseKezzAccess");
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            int passwordIterations = 2;

            var password = new PasswordDeriveBytes
            (
                "KezzPassPhaseKezzAccess",
                saltValueBytes,
                "SHA256",
                passwordIterations
            );

            byte[] keyBytes = password.GetBytes(keySize / 8);

            var symmetricKey = new RijndaelManaged
            {
                Mode = CipherMode.CBC
            };

            ICryptoTransform encryptor = symmetricKey.CreateEncryptor
            (
                keyBytes,
                initVectorBytes
            );

            var memoryStream = new MemoryStream();

            CryptoStream cryptoStream = new CryptoStream
            (
                memoryStream,
                encryptor,
                CryptoStreamMode.Write
            );

            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] cipherTextBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();
            password.Dispose();
            symmetricKey.Dispose();

            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string Decrypt(string cipherText, int keySize)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes("!1A3g2D4s9K556g7");
            byte[] saltValueBytes = Encoding.ASCII.GetBytes("KezzPassPhaseKezzAccess");
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            int passwordIterations = 2;

            var password = new PasswordDeriveBytes
            (
                "KezzPassPhaseKezzAccess",
                saltValueBytes,
                "SHA256",
                passwordIterations
            );

            byte[] keyBytes = password.GetBytes(keySize / 8);

            var symmetricKey = new RijndaelManaged
            {
                Mode = CipherMode.CBC
            };

            ICryptoTransform decryptor = symmetricKey.CreateDecryptor
            (
                keyBytes,
                initVectorBytes
            );

            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream
            (
                memoryStream,
                decryptor,
                CryptoStreamMode.Read
            );
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read
            (
                plainTextBytes,
                0,
                plainTextBytes.Length
            );

            memoryStream.Close();
            cryptoStream.Close();
            symmetricKey.Dispose();
            password.Dispose();

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
        #endregion
    }
}
