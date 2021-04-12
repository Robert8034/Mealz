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

        public void AddUser(Guid userId, string email, string password)
        {
            var salt = _cryptographyService.GenerateSalt();

            var hashedPassword = _cryptographyService.HashPassword(password, salt);

            var stringSalt = Convert.ToBase64String(salt);

            _userContext.Users.Add(new Models.User { UserId = userId, Email = email, Password = hashedPassword, Salt = stringSalt });
            _userContext.SaveChanges();
        }

        public string Authenticate(string email, string password)
        {
            var user = _userContext.Users.FirstOrDefault(e => e.Email == email);

            if (user != null && user.Password == _cryptographyService.HashInput(password, user.Salt))
            {
                Guid id = _userContext.Users.FirstOrDefault(e => e.Email == email).UserId;
                return _jwtAuthenticationManager.WriteToken(id);
            }

            return null;
        }

        public void ChangeUser(Guid userId, string email)
        {
            var user = _userContext.Users.FirstOrDefault(e => e.UserId == userId);

            if (user != null)
            {
                user.Email = email;
                _userContext.SaveChanges();
            }
        }
    }
}
