using Authentication.DAL;
using Authentication.Models;
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
        private readonly IUserDAL _userDAL;

        private readonly ICryptographyService _cryptographyService;

        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;

        public UserService(IUserDAL userDAL, ICryptographyService cryptographyService, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            _userDAL = userDAL;
            _cryptographyService = cryptographyService;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        public void AddUser(Guid userId, string email, string password)
        {
            var salt = _cryptographyService.GenerateSalt();

            var hashedPassword = _cryptographyService.HashPassword(password, salt);

            var stringSalt = Convert.ToBase64String(salt);

            _userDAL.AddUser(new User { UserId = userId, Email = email, Password = hashedPassword, Salt = stringSalt, Role = Roles.User });
        }

        public string Authenticate(string email, string password)
        {
            var user = _userDAL.GetUserByEmail(email);

            if (user != null && user.Password == _cryptographyService.HashInput(password, user.Salt))
            {
                var result = _userDAL.GetUserByEmail(email);
                return _jwtAuthenticationManager.WriteToken(result.UserId, result.Role);
            }

            return null;
        }

        public void ChangeUser(Guid userId, string email)
        {
            var user = _userDAL.GetUserById(userId);

            if (user != null)
            {
                user.Email = email;
                _userDAL.Save();
            }
        }

        public void ChangeUserRole(User user, Roles role)
        {
            user.Role = role;

            _userDAL.Save();
        }

        public User GetUser(Guid userId)
        {
            return _userDAL.GetUserById(userId);
        }

        public void DeleteUser(Guid userId)
        {
            var user = _userDAL.GetUserById(userId);

            if (user != null)
            {
                _userDAL.DeleteUser(user);
            }
        }

        public async Task ConfigureAdmin(Guid userId)
        {
            var salt = _cryptographyService.GenerateSalt();

            var hashedPassword = _cryptographyService.HashPassword("Admin123", salt);

            var stringSalt = Convert.ToBase64String(salt);

            await _userDAL.AddUser(new User { UserId = userId, Email = "Admin@Admin.com", Password = hashedPassword, Salt = stringSalt, Role = Roles.Admin });
        }
    }
}
