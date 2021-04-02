using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public class CrypthographyService : ICryptographyService
    {
        public string HashPassword(string password, byte[] salt)
        {
            var pbdkf2 = new Rfc2898DeriveBytes(password, salt);

            byte[] hash = pbdkf2.GetBytes(24);

            return Convert.ToBase64String(hash);
        }

        public byte[] GenerateSalt()
        {
            var salt = new byte[24];

            new RNGCryptoServiceProvider().GetBytes(salt);

            return salt;
        }

        public string HashInput(string password, string salt)
        {
            var byteSalt = Convert.FromBase64String(salt);

            var pbdkf2 = new Rfc2898DeriveBytes(password, byteSalt);

            byte[] hash = pbdkf2.GetBytes(24);

            return Convert.ToBase64String(hash);
        }
    }
}
