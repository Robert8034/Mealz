using Authentication.Models;
using Authentication.Services;
using Authentication.Tests.MockServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Authentication.Tests.UnitTests
{
    public class UserServiceTests
    {
        [Fact]
        public void AddUserSuccesTest()
        {
            //Arrange
            MockUserDAL mockUserDAL = new MockUserDAL();
            UserService userService = new UserService(mockUserDAL, new MockCrypthographyService(), new MockJwtAuthenticationManager());

            //Act
            var id = Guid.NewGuid();
            userService.AddUser(id, "Test@mail.com", "Password");

            //Assert
            Assert.Equal(id, mockUserDAL.users.First().UserId);
        }

        [Fact]
        public void AuthenticateSuccesTest()
        {
            //Arrange
            MockUserDAL mockUserDAL = new MockUserDAL();
            UserService userService = new UserService(mockUserDAL, new MockCrypthographyService(), new MockJwtAuthenticationManager());

            var id = Guid.NewGuid();
            User user = new User { UserId = id, Email = "Email@Email.com", Password = "12345", Role = Roles.User };
            mockUserDAL.users.Add(user);

            //Act
            string result = userService.Authenticate("Email@Email.com", "12345");

            //Assert
            Assert.Equal("Token", result);
        }


        [Fact]
        public void AuthenticateFailureTest()
        {
            //Arrange
            MockUserDAL mockUserDAL = new MockUserDAL();
            UserService userService = new UserService(mockUserDAL, new MockCrypthographyService(), new MockJwtAuthenticationManager());

            var id = Guid.NewGuid();
            User user = new User { UserId = id, Email = "Email@Email.com", Password = "12345", Role = Roles.User };
            mockUserDAL.users.Add(user);

            //Act
            string result = userService.Authenticate("Email@Email.com", "xxxxx");

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void ChangeUserSuccesTest()
        {
            //Arrange
            MockUserDAL mockUserDAL = new MockUserDAL();
            UserService userService = new UserService(mockUserDAL, new MockCrypthographyService(), new MockJwtAuthenticationManager());

            var id = Guid.NewGuid();
            User user = new User { UserId = id, Email = "Email@Email.com", Password = "12345", Role = Roles.User };
            mockUserDAL.users.Add(user);

            //Act
            userService.ChangeUser(id, "New@Email.com");

            //Assert
            Assert.Equal("New@Email.com", mockUserDAL.users.First().Email);
            Assert.Equal(id, mockUserDAL.users.First().UserId);
        }

        [Fact]
        public void ChangeUserNoChangeTest()
        {
            //Arrange
            MockUserDAL mockUserDAL = new MockUserDAL();
            UserService userService = new UserService(mockUserDAL, new MockCrypthographyService(), new MockJwtAuthenticationManager());

            var id = Guid.NewGuid();
            User user = new User { UserId = id, Email = "Email@Email.com", Password = "12345", Role = Roles.User };
            mockUserDAL.users.Add(user);

            //Act
            userService.ChangeUser(id, "Email@Email.com");

            //Assert
            Assert.Equal("Email@Email.com", mockUserDAL.users.First().Email);
            Assert.Equal(id, mockUserDAL.users.First().UserId);
        }


        [Fact]
        public void ChangeUserNotExistTest()
        {
            //Arrange
            MockUserDAL mockUserDAL = new MockUserDAL();
            UserService userService = new UserService(mockUserDAL, new MockCrypthographyService(), new MockJwtAuthenticationManager());

            var id = Guid.NewGuid();
            User user = new User { UserId = id, Email = "Email@Email.com", Password = "12345", Role = Roles.User };
            mockUserDAL.users.Add(user);

            //Act
            userService.ChangeUser(Guid.NewGuid(), "Email@Email.com");

            //Assert
            Assert.Equal("Email@Email.com", mockUserDAL.users.First().Email);
            Assert.Equal(id, mockUserDAL.users.First().UserId);
        }

        [Fact]
        public void ChangeUserRoleSuccesTest()
        {
            //Arrange
            MockUserDAL mockUserDAL = new MockUserDAL();
            UserService userService = new UserService(mockUserDAL, new MockCrypthographyService(), new MockJwtAuthenticationManager());

            var id = Guid.NewGuid();
            User user = new User { UserId = id, Email = "Email@Email.com", Password = "12345", Role = Roles.User };
            mockUserDAL.users.Add(user);

            //Act
            userService.ChangeUserRole(user, Roles.Moderator);

            //Assert
            Assert.Equal(Roles.Moderator, mockUserDAL.users.First().Role);
            Assert.Equal(id, mockUserDAL.users.First().UserId);
        }

        [Fact]
        public void GetUserSuccesTest()
        {
            //Arrange
            MockUserDAL mockUserDAL = new MockUserDAL();
            UserService userService = new UserService(mockUserDAL, new MockCrypthographyService(), new MockJwtAuthenticationManager());

            var id = Guid.NewGuid();
            User user = new User { UserId = id, Email = "Email@Email.com", Password = "12345", Role = Roles.User };
            mockUserDAL.users.Add(user);

            //Act
            var result = userService.GetUser(id);

            //Assert
            Assert.Equal(id, result.UserId);
        }

        [Fact]
        public void GetUserFailureTest()
        {
            //Arrange
            MockUserDAL mockUserDAL = new MockUserDAL();
            UserService userService = new UserService(mockUserDAL, new MockCrypthographyService(), new MockJwtAuthenticationManager());

            var id = Guid.NewGuid();
            User user = new User { UserId = id, Email = "Email@Email.com", Password = "12345", Role = Roles.User };
            mockUserDAL.users.Add(user);

            //Act
            var result = userService.GetUser(Guid.NewGuid());

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void DeleteUserSuccesTest()
        {
            //Arrange
            MockUserDAL mockUserDAL = new MockUserDAL();
            UserService userService = new UserService(mockUserDAL, new MockCrypthographyService(), new MockJwtAuthenticationManager());

            var id = Guid.NewGuid();
            User user = new User { UserId = id, Email = "Email@Email.com", Password = "12345", Role = Roles.User };
            mockUserDAL.users.Add(user);

            //Act
            userService.DeleteUser(id);

            //Assert
            Assert.Null(mockUserDAL.users.FirstOrDefault(e => e.UserId == id));
        }

        [Fact]
        public void DeleteUserFailureTest()
        {
            //Arrange
            MockUserDAL mockUserDAL = new MockUserDAL();
            UserService userService = new UserService(mockUserDAL, new MockCrypthographyService(), new MockJwtAuthenticationManager());

            var id = Guid.NewGuid();
            User user = new User { UserId = id, Email = "Email@Email.com", Password = "12345", Role = Roles.User };
            mockUserDAL.users.Add(user);

            //Act
            userService.DeleteUser(Guid.NewGuid());

            //Assert
            Assert.Single(mockUserDAL.users);
        }
    }
}
