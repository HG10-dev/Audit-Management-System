using AuthorizationService.Models;
using System.Collections.Generic;

namespace AuthorizationService.Provider
{
    public abstract class IAuthProvider
    {
        public abstract AuthCredentials GetAuthUser(AuthCredentials credentials);
        public abstract List<AuthCredentials> GetUsers();
    }
}