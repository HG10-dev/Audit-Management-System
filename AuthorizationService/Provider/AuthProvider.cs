
using AuthorizationService.Models;
using System.Collections.Generic;
using System.Linq;

namespace AuthorizationService.Provider
{
    public class AuthProvider : IAuthProvider
    {
        private readonly List<AuthCredentials> credList ;

        public AuthProvider()
        {
            credList = new List<AuthCredentials>()
            {
                new AuthCredentials { UserName = "user1", Password = "pass1" },
                new AuthCredentials { UserName = "user", Password = "pass2" }
            };
        }
        public  List<AuthCredentials> GetUsers() { return credList; }
        public AuthCredentials GetAuthUser(AuthCredentials credentials)
        {
            if (credentials == null) { return null; }
            List<AuthCredentials> userList = GetUsers();
            AuthCredentials authUser = userList.FirstOrDefault(c => c.UserName == credentials.UserName && c.Password == credentials.Password);
            return authUser;
        }
    }
}
