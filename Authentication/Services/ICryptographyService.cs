using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public interface ICryptographyService
    {
        /// <summary>
        /// Hashes a password with a given salt
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        string HashPassword(string password, byte[] salt);
        /// <summary>
        /// Generates a random salt
        /// </summary>
        /// <returns></returns>
        byte[] GenerateSalt();
        /// <summary>
        /// Hashes the input password with a retrieved salt from the Authentication database
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        string HashInput(string password, string salt);
    }
}
