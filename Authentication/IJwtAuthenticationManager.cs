using Authentication.Models;
using System;

namespace Authentication
{
    public interface IJwtAuthenticationManager
    {
        string WriteToken(Guid id, Roles role);
        Guid ReadToken(string token);
        string GetRole(string token);
    }
}
