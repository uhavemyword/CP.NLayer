// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 1/17/2013 1:48:00 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Common
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Utility class that handles encryption
    /// </summary>
    public static class AESEncryption
    {
        /// <summary>
        /// Encrypts a string
        /// </summary>
        /// <param name="plainText">Text to be encrypted</param>
        /// <param name="salt">Salt to encrypt with</param>
        /// <param name="password">Password to encrypt with</param>
        /// <param name="keySize"> Can be 128, 192, or 256</param>
        /// <param name="dynamicCipherText">Dynamic ciphertext for same plaintext</param>
        /// <returns>An encrypted string</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static string Encrypt(string plainText, string salt, string password = "asdfQWER1234!@#$", int keySize = 256, bool dynamicCipherText = true)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return string.Empty;
            }
            salt = salt == null ? string.Empty : salt;

            byte[] saltBytes = Encoding.ASCII.GetBytes(salt);
            byte[] cipherTextBytes = null;

            using (RijndaelManaged symmetricKey = new RijndaelManaged())
            {
                if (dynamicCipherText)
                {
                    symmetricKey.Padding = PaddingMode.ISO10126;
                }
                symmetricKey.Mode = CipherMode.CBC;
                PasswordDeriveBytes derivedPassword = new PasswordDeriveBytes(password, saltBytes, "SHA1", 5);
                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(derivedPassword.GetBytes(keySize / 8), derivedPassword.GetBytes(16));
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }
                        cipherTextBytes = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        /// <summary>
        /// Decrypts a string
        /// </summary>
        /// <param name="cipherText">Text to be decrypted</param>
        /// <param name="salt">Salt to decrypt with</param>
        /// <param name="password">Password to encrypt with</param>
        /// <param name="keySize"> Can be 128, 192, or 256</param>
        /// <param name="dynamicCipherText">Dynamic ciphertext for same plaintext</param>
        /// <returns>A decrypted string</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        public static string Decrypt(string cipherText, string salt, string password = "asdfQWER1234!@#$", int keySize = 256, bool dynamicCipherText = true)
        {
            if (string.IsNullOrEmpty(cipherText))
            {
                return string.Empty;
            }
            salt = salt == null ? string.Empty : salt;

            byte[] saltBytes = Encoding.ASCII.GetBytes(salt);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            string plaintext = null;

            using (RijndaelManaged symmetricKey = new RijndaelManaged())
            {
                if (dynamicCipherText)
                {
                    symmetricKey.Padding = PaddingMode.ISO10126;
                }
                symmetricKey.Mode = CipherMode.CBC;
                PasswordDeriveBytes derivedPassword = new PasswordDeriveBytes(password, saltBytes, "SHA1", 5);
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(derivedPassword.GetBytes(keySize / 8), derivedPassword.GetBytes(16));
                using (MemoryStream emoryStream = new MemoryStream(cipherTextBytes))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(emoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            plaintext = streamReader.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}