using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Tests.UnitTests
{
    [TestFixture]
    public class JwtAuthenticationManagerTests
    {
        [Test]
        public void WriteTokenTest()
        {
            //Arrange
            JwtAuthenticationManager jwtAuthenticationManager = new JwtAuthenticationManager();

            //Act
            var result = jwtAuthenticationManager.WriteToken(Guid.NewGuid(), Models.Roles.User);

            //Assert
            Assert.NotNull(result);
        }

        [Test]
        public void ReadTokenTest()
        {
            //Arrange
            JwtAuthenticationManager jwtAuthenticationManager = new JwtAuthenticationManager();

            var id = Guid.NewGuid();

            var token = jwtAuthenticationManager.WriteToken(id, Models.Roles.User);

            //Act
            var result = jwtAuthenticationManager.ReadToken(token);

            //Assert
            Assert.AreEqual(id, result);
        }

        [Test]
        public void GetRoleTest()
        {
            //Arrange
            JwtAuthenticationManager jwtAuthenticationManager = new JwtAuthenticationManager();

            var id = Guid.NewGuid();

            var token = jwtAuthenticationManager.WriteToken(id, Models.Roles.User);

            //Act
            var result = jwtAuthenticationManager.GetRole(token);

            //Assert
            Assert.AreEqual(Models.Roles.User.ToString(), result);
        }
    }
}
