using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BusinessSolutionsLayer.Services
{
    public class CrytpoService : ICrytpoService
    {
        public string GetHash(string toHash)
        {
            using (var cref = SHA512.Create())
            {
                return Convert.ToBase64String(cref.ComputeHash(Encoding.UTF8.GetBytes(toHash)));
            }
        }

        public string GenerateSalt(int keyLength)
        {
            return Convert.ToBase64String(GenerateRandomCryptographicBytes(keyLength));
        }

        public byte[] GenerateRandomCryptographicBytes(int keyLength)
        {
            var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[keyLength];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return randomBytes;
        }

    }
}
