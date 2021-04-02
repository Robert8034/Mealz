using Authentication.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext _userContext;

        private readonly ICryptographyService _cryptographyService;

        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public UserService(UserContext userContext, ICryptographyService cryptographyService, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _userContext = userContext;
            _cryptographyService = cryptographyService;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        public void AddUser(string email, string password)
        {
            var salt = _cryptographyService.GenerateSalt();

            var hashedPassword = _cryptographyService.HashPassword(password, salt);

            var stringSalt = Convert.ToBase64String(salt);

            _userContext.Users.Add(new Models.User { Email = email, Password = hashedPassword, Salt = stringSalt });
            _userContext.SaveChanges();
        }

        public string Authenticate(string email, string password)
        {
            var user = _userContext.Users.FirstOrDefault(e => e.Email == email);

            if (user != null && user.Password == _cryptographyService.HashInput(password, user.Salt))
            {
                return _jwtAuthenticationManager.WriteToken(email);
            }

            return null;
        }
    }
}
