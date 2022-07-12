using AuthorizationService.Models;
using AuthorizationService.Provider;
using AuthorizationService.Repository;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthorizationTest
{
    internal class AuthRepoTest
    {
        AuthCredentials user = new AuthCredentials();
        readonly IConfiguration config;      
        AuthRepo auth;

        [SetUp]
        public void Setup()
        {
            user = new AuthCredentials()
            {
                UserName = "user1", Password = "pass1" 
            };
            auth = new AuthRepo( config);
        }


        [Test]
        public void GeneratesJWT()
        {
            AuthCredentials cred = new AuthCredentials { UserName = "user1", Password = "pass"};
            Mock<IConfiguration> mock = new Mock<IConfiguration>();
            //mock.Setup(c => c.GetBy(It.IsAny<AuthCredentials>())).Returns("token");
            //config.Setup(c => c["Jwt:Issuer"]).Returns("thisIsMySecretKey");

            mock.SetupGet(x => x[It.Is<string>(s => s == "Jwt:Key")]).Returns("youDon'tNeedToKnowMySecretKey");
            //mock.SetupGet(x => x[It.Is<string>(s => s == "ValidPowerStatus")]).Returns("On");

            AuthRepo authNew = new AuthRepo(mock.Object);

            string token = authNew.GenerateJSONWebToken(cred);

            Assert.IsNotNull(token);
        }

        [Test]
        public void FailsToGenerateJWT()
        {
            Mock<IConfiguration> mock = new Mock<IConfiguration>();
            mock.Setup(c => c["Jwt: Key"]).Returns("thisIsMySecretKey");
            string token = auth.GenerateJSONWebToken(null);

            Assert.IsNull(token);
        }
    }
}
