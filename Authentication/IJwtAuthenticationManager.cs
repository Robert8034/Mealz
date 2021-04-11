using System;

namespace Authentication
{
    public interface IJwtAuthenticationManager
    {
        string WriteToken(Guid id);
        Guid ReadToken(string token);
    }
}
