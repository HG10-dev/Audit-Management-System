using System.ComponentModel.DataAnnotations;

namespace AuditPortal.Models
{
    public class AuditRequest
    {
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public string ProjectManagerName { get; set; }
        [Required]
        public string ApplicationOwnerName  { get; set; }
        [Required]
        public AuditDetail AuditDetail { get; set; }
    }
}
