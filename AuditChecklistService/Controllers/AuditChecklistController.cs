using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using AuditChecklistModule.Models;
using AuditChecklistModule.Providers;
using AuditChecklistModule.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuditChecklistModule.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuditChecklistController : ControllerBase
    {
        private readonly IChecklistProvider provider;      
                 
        public AuditChecklistController(IChecklistProvider _provider)
        {
            provider = _provider;
        }

        // GET: api/AuditChecklist
        [HttpGet("{auditType}")]
        public IActionResult Get(string auditType)
        {
            if (string.IsNullOrEmpty(auditType))
                return BadRequest("No Input");

            if ((auditType !="Internal") && (auditType !="SOX"))
                return Ok("Wrong Input");

            try
            {
                List<Questions> list = provider.QuestionsProvider(auditType);
                    return Ok(list);
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
        }        
    }
}