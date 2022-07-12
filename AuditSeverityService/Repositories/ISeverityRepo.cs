using AuditSeverityService.Models;
using System.Threading.Tasks;

namespace AuditSeverityService.Repositories
{
    public interface ISeverityRepo
    {
        public Task<AuditResponse> GetResponse( AuditRequest request);
    }
}
