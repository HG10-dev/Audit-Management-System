using AuthorizationService.Models;

namespace AuthorizationService.Repository
{
    public interface IAuthRepo
    {      
        public string GenerateJSONWebToken(AuthCredentials cred);
    }
}