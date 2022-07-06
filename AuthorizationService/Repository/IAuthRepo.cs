using AuthorizationService.Models;

namespace AuthorizationService.Repository
{
    public abstract class IAuthRepo
    {        
        public abstract string GenerateJSONWebToken(AuthCredentials cred);
    }
}
