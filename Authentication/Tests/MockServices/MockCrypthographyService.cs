using Authentication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Tests.MockServices
{
    public class MockCrypthographyService : ICryptographyService
    {
        public byte[] GenerateSalt()
        {
            return new byte[1];
        }

        public string HashInput(string password, string salt)
        {
            return password;
        }

        public string HashPassword(string password, byte[] salt)
        {
            return password;
        }
    }
}
