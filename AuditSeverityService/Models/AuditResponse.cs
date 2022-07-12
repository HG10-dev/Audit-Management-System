namespace AuditSeverityService.Models
{
    public class AuditResponse
    {
        public int AuditId { get; set; }
        public Status ProjectExecutionStatus { get; set; }
        public int RemedialActionDuration { get; set; }
        
    }
    public enum Status
    {
        Red,
        Green,
    }
}
