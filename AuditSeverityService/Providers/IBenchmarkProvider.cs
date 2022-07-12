using AuditSeverityService.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuditSeverityService.Providers
{
    public interface IBenchmarkProvider
    {
        public Task<Dictionary<string, int>> GetAuditBenchmarks(IConfiguration _config);
    }
}
