using AuditSeverityService.Models;
using AuditSeverityService.Providers;
using AuditSeverityService.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace AuditSeverityService.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuditSeverityController : Controller
    {
        private IConfiguration config;
        private ISeverityRepo repo;
        private IBenchmarkProvider provider;
        public AuditSeverityController(IConfiguration _config, IBenchmarkProvider _provider, ISeverityRepo _repo)
        {
            config = _config;
            provider = _provider;
            repo = _repo;
        }

        [HttpPost]
        public async Task<IActionResult> ProjectExecutionStatus([FromBody] AuditRequest request)
        {
            if(request == null)
            {
                return BadRequest();
            }
            AuditRequest input = new AuditRequest()
            {
                ProjectName = request.ProjectName,
                ProjectManagerName = request.ProjectManagerName,
                ApplicationOwnerName = request.ApplicationOwnerName,
                AuditDetail = new AuditDetail()
                {
                    AuditDate = request.AuditDetail.AuditDate,
                    AuditType = request.AuditDetail.AuditType,
                    AuditQuestions = request.AuditDetail.AuditQuestions,
                }
            };

            AuditResponse response = await repo.GetResponse(input);

            if(response == null)
            {
                return StatusCode(500);
            }

            return Ok(response);
        }
    }
}
