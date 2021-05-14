using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using UserService.Test.MockServices;

namespace UserService.Test.UnitTests
{
    [TestFixture]
    class UserServiceTests
    {
        [Test]
        public void CheckCredentialsSuccesTest()
        {
            //Arrange
            MockUserDAL mockUserDAL = new MockUserDAL();
            mockUserDAL.users.Add(new Models.User { Email = "Email1@Mail.com" });

            Services.UserService userService = new Services.UserService(mockUserDAL, new MockMessagePublisher());
            //Act
            var result = userService.CheckCredentials("Email2@Mail.com", "Email2@Mail.com", "Password", "Password");

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CheckCredentialsFailureTest()
        {
            //Arrange
            MockUserDAL mockUserDAL = new MockUserDAL();
            mockUserDAL.users.Add(new Models.User { Email = "Email1@Mail.com" });

            Services.UserService userService = new Services.UserService(mockUserDAL, new MockMessagePublisher());
            //Act
            var result = userService.CheckCredentials("Email1@Mail.com", "Email1@Mail.com", "Password", "Password");

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void RegisterUserSuccesTest()
        {
            //Arrange
            MockUserDAL mockUserDAL = new MockUserDAL();
            Services.UserService userService = new Services.UserService(mockUserDAL, new MockMessagePublisher());

            //Act
            var result = userService.Register(Guid.NewGuid(), "Mail@Mail.com", "Robert", "Hoi ik ben Robert");

            //Assert
            Assert.AreEqual(1, mockUserDAL.users.Count);
            Assert.AreEqual("Mail@Mail.com", mockUserDAL.users[0].Email);
            Assert.AreEqual("Robert", mockUserDAL.users[0].DisplayName);
            Assert.AreEqual("Hoi ik ben Robert", mockUserDAL.users[0].Biography);
        }

        [Test]
        public void GetUserSuccesTest()
        {
            //Arrange
            MockUserDAL mockUserDAL = new MockUserDAL();
            var id = Guid.NewGuid();
            mockUserDAL.users.Add(new Models.User { Email = "Email1@Mail.com", UserId = id });

            Services.UserService userService = new Services.UserService(mockUserDAL, new MockMessagePublisher());

            //Act
            var result = userService.GetUser(id);

            //Assert
            Assert.NotNull(result);
            Assert.AreEqual("Email1@Mail.com", result.Email);
            Assert.AreEqual(id, result.UserId);
        }

        [Test]
        public void GetUserFailureTest()
        {
            //Arrange
            MockUserDAL mockUserDAL = new MockUserDAL();
            var id = Guid.NewGuid();
            mockUserDAL.users.Add(new Models.User { Email = "Email1@Mail.com", UserId = id });

            Services.UserService userService = new Services.UserService(mockUserDAL, new MockMessagePublisher());

            //Act
            var result = userService.GetUser(Guid.NewGuid());

            //Assert
            Assert.Null(result);
        }

        [Test]
        public void UpdateUserSuccesTest()
        {
            //Arrange
            MockUserDAL mockUserDAL = new MockUserDAL();
            var id = Guid.NewGuid();
            mockUserDAL.users.Add(new Models.User { Email = "Email1@Mail.com", UserId = id });
            mockUserDAL.users.Add(new Models.User { Email = "Email2@Mail.com", UserId = Guid.NewGuid() });

            Services.UserService userService = new Services.UserService(mockUserDAL, new MockMessagePublisher());

            //Act
            var result = userService.UpdateUser(new Models.User { UserId = id, Email = "New@Mail.com" });

            //Assert
            Assert.IsTrue(result.Result);
        }

        [Test]
        public void UpdateUserFailureTest()
        {
            //Arrange
            MockUserDAL mockUserDAL = new MockUserDAL();
            var id = Guid.NewGuid();
            mockUserDAL.users.Add(new Models.User { Email = "Email1@Mail.com", UserId = id });
            mockUserDAL.users.Add(new Models.User { Email = "Email2@Mail.com", UserId = Guid.NewGuid() });

            Services.UserService userService = new Services.UserService(mockUserDAL, new MockMessagePublisher());

            //Act
            var result = userService.UpdateUser(new Models.User { UserId = id, Email = "Email2@Mail.com" });

            //Assert
            Assert.IsFalse(result.Result);
        }

        [Test]
        public void DeleteUserSuccesTest()
        {
            //Arrange
            MockUserDAL mockUserDAL = new MockUserDAL();

            var id = Guid.NewGuid();
            Models.User user = new Models.User { Email = "Email1@Mail.com", UserId = id };

            mockUserDAL.users.Add(user);

            Services.UserService userService = new Services.UserService(mockUserDAL, new MockMessagePublisher());

            //Act
            var result = userService.DeleteUser(user);

            //Assert
            Assert.True(result.Result);
            Assert.AreEqual(0, mockUserDAL.users.Count);
        }

        [Test]
        public void DeleteUserFailureTest()
        {
            //Arrange
            MockUserDAL mockUserDAL = new MockUserDAL();

            var id = Guid.NewGuid();
            Models.User user = new Models.User { Email = "Email1@Mail.com", UserId = id };

            mockUserDAL.users.Add(user);

            Services.UserService userService = new Services.UserService(mockUserDAL, new MockMessagePublisher());

            //Act
            var result = userService.DeleteUser(new Models.User { UserId = Guid.NewGuid()});

            //Assert
            Assert.True(result.Result);
            Assert.AreEqual(1, mockUserDAL.users.Count);
        }
    }
}
