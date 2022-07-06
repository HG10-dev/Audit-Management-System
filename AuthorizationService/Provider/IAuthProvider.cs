using AuthorizationService.Models;
using System.Collections.Generic;

namespace AuthorizationService.Provider
{
    public interface IAuthProvider
    {
        public AuthCredentials GetAuthUser(AuthCredentials credentials);
        public List<AuthCredentials> GetUsers();
    }
}