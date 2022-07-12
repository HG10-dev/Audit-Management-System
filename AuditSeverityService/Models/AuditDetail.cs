using System;
using System.Collections.Generic;

namespace AuditSeverityService.Models
{
    public class AuditDetail
    {
        public AuditType AuditType { get; set; }
        public string AuditDate { get; set; }
        public List<QAndA> AuditQuestions { get; set; }
    }

    public class QAndA
    {
        public int Id { get; set; }
        public string Ans { get; set; }
    }

    public enum AuditType { Internal, SOX}
    public enum YesNo { No, Yes}
}