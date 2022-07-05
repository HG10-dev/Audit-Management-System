using AuthorizationService.Models;

namespace AuthorizationService.Repository
{
    public abstract class IAuthRepo
    {
        public abstract AuthCredentials Authenticate(AuthCredentials cred);
        public abstract string GenerateJSONWebToken(AuthCredentials cred);
    }
}