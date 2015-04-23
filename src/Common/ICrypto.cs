// ------------------------------------------------------------------------------------
//      Copyright (c) 2012 uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Common
{
    public interface ICrypto
    {
        string Encrypt(string plaintext, string password);

        bool IsMatch(string plaintext, string ciphertext, string password);
    }
}