using Authentication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xunit;

namespace Authentication.Tests.UnitTests
{
    public class CrypthographyServiceTests
    {
        [Fact]
        public void HashPasswordTest()
        {
            //Arrange
            CrypthographyService crypthographyService = new CrypthographyService();

            byte[] byteArray = new byte[24];

            var pbdkf2 = new Rfc2898DeriveBytes("12345", byteArray);

            byte[] hash = pbdkf2.GetBytes(24);

            string expected = Convert.ToBase64String(hash);

            //Act
            var result = crypthographyService.HashPassword("12345", byteArray);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GenerateSaltTest()
        {
            //Arrange
            CrypthographyService crypthographyService = new CrypthographyService();

            //Act
            var result = crypthographyService.GenerateSalt();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void HashInputTest()
        {
            //Arrange
            CrypthographyService crypthographyService = new CrypthographyService();

            var salt = Convert.ToBase64String(new byte[24]);

            var byteSalt = Convert.FromBase64String(salt);

            var pbdkf2 = new Rfc2898DeriveBytes("12345", byteSalt);

            byte[] hash = pbdkf2.GetBytes(24);

            var exptected = Convert.ToBase64String(hash);

            //Act
            var result = crypthographyService.HashInput("12345", salt);

            //Assert
            Assert.Equal(exptected, result);
        }
    }
}
