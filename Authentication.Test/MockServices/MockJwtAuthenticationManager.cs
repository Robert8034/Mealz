using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Tests.MockServices
{
    public class MockJwtAuthenticationManager : IJwtAuthenticationManager
    {
        public string GetRole(string token)
        {
            return "User";
        }

        public Guid ReadToken(string token)
        {
            return new Guid();
        }

        public string WriteToken(Guid id, Roles role)
        {
            return "Token";
        }
    }
}
