using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Services
{
    public interface IUserService
    {
        bool checkCredentials(string email, string emailConfirm, string password, string passwordConfirm);

        void register(string email, string password);
    }
}
