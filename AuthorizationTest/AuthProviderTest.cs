using System;
using System.Collections.Generic;
using System.Text;
using AuthorizationService.Models;
using AuthorizationService.Provider;
using Moq;
using NUnit.Framework;

namespace AuthorizationTest
{
    internal class AuthProviderTest
    {
        List<AuthCredentials> user = new List<AuthCredentials>();

        [SetUp]
        public void Setup()
        {
            user = new List<AuthCredentials>()
            {
                new AuthCredentials{UserName = "user1", Password = "pass1"}
            };
        }

        [TestCase("user1","pass1")]
        [TestCase("user2","pass2")]
        public void GetAuthUserReturnsObject(string name, string pass)
        {
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();   
            mock.Setup(a => a.GetUsers()).Returns(user);
            AuthProvider auth = new AuthProvider();
            AuthCredentials cred = new AuthCredentials { UserName = name, Password = pass };
            
            var authCred = auth.GetAuthUser(cred);
            Assert.IsNotNull(authCred);
        }
        
        [TestCase("user1","user1")]
        [TestCase("pass1","pass1")]
        public void GetAuthUserReturnsNull(string name, string pass)
        {
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();   
            mock.Setup(a => a.GetUsers()).Returns(user);
            AuthProvider auth = new AuthProvider();
            AuthCredentials cred = new AuthCredentials { UserName = name, Password = pass };
            
            var authCred = auth.GetAuthUser(cred);
            Assert.IsNull(authCred);
        }
    }
}
