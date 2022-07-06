using System.ComponentModel.DataAnnotations;

namespace AuthorizationService.Models
{
    public class AuthCredentials
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
