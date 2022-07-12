using AuditBenchmarkService.Models;
using System.Collections.Generic;

namespace AuditBenchmarkService.Repository
{
    public interface IBenchmarkRepo
    {
        public List<AuditBenchmark> GetNolist();
    }
}
