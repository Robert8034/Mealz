using Authentication.Models;
using System;

namespace Authentication
{
    public interface IJwtAuthenticationManager
    {
        /// <summary>
        /// Writes a token based on the given Guid and Role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        string WriteToken(Guid id, Roles role);
        /// <summary>
        /// Reads a JWT Token and returns the matching Guid
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Guid ReadToken(string token);
        /// <summary>
        /// Reads a JWT Token and returns the matching Role in a String format
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        string GetRole(string token);
    }
}
