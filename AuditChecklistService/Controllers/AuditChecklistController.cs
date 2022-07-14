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
        private readonly IChecklistProvider checklistProviderobj;      
                 
        public AuditChecklistController(IChecklistProvider _checklistProviderobj)
        {
            checklistProviderobj = _checklistProviderobj;
        }

        // GET: api/AuditChecklist
        [HttpGet("{auditType}")]
        public IActionResult GetQuestions(string auditType)
        {
            if (string.IsNullOrEmpty(auditType))
                return BadRequest("No Input");

            if ((auditType !="Internal") && (auditType !="SOX"))
                return Ok("Wrong Input");

            try
            {
                List<Questions> list = checklistProviderobj.QuestionsProvider(auditType);
                    return Ok(list);
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
        }        
    }
}