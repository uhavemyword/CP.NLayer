// ------------------------------------------------------------------------------------
//      Copyright (c) 2012 uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Common
{
    public class Crypto : ICrypto
    {
        public string Encrypt(string plaintext, string salt)
        {
            var result = AESEncryption.Encrypt(plaintext, salt);
            return result;
        }

        public bool IsMatch(string plaintext, string ciphertext, string salt)
        {
            var result = plaintext == AESEncryption.Decrypt(ciphertext, salt);
            return result;
        }
    }
}