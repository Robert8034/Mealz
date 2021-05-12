using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Authentication.Tests.UnitTests
{
    public class JwtAuthenticationManagerTests
    {
        [Fact]
        public void WriteTokenTest()
        {
            //Arrange
            JwtAuthenticationManager jwtAuthenticationManager = new JwtAuthenticationManager();

            //Act
            var result = jwtAuthenticationManager.WriteToken(Guid.NewGuid(), Models.Roles.User);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ReadTokenTest()
        {
            //Arrange
            JwtAuthenticationManager jwtAuthenticationManager = new JwtAuthenticationManager();

            var id = Guid.NewGuid();

            var token = jwtAuthenticationManager.WriteToken(id, Models.Roles.User);

            //Act
            var result = jwtAuthenticationManager.ReadToken(token);

            //Assert
            Assert.Equal(id, result);
        }

        [Fact]
        public void GetRoleTest()
        {
            //Arrange
            JwtAuthenticationManager jwtAuthenticationManager = new JwtAuthenticationManager();

            var id = Guid.NewGuid();

            var token = jwtAuthenticationManager.WriteToken(id, Models.Roles.User);

            //Act
            var result = jwtAuthenticationManager.GetRole(token);

            //Assert
            Assert.Equal(Models.Roles.User.ToString(), result);
        }
    }
}
