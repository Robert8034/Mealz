using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Services
{
    public interface IUserService
    {
        bool CheckCredentials(string email, string emailConfirm, string password, string passwordConfirm);

        void Register(string email, string password, string displayName, string biography);
    }
}
