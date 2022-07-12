using System.ComponentModel.DataAnnotations;

namespace AuditPortal.Models
{
    public class AuthCredentials
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
