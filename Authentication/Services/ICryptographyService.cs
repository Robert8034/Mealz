using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public interface ICryptographyService
    {
        string HashPassword(string password, byte[] salt);
        byte[] GenerateSalt();
        string HashInput(string password, string salt);
    }
}
