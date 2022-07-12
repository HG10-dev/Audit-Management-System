using AuditBenchmarkService.Models;
using AuditBenchmarkService.Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AuditBenchmarkService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AuditBenchmarkController : ControllerBase
    {
        private readonly IBenchmarkProvider objProvider;
        public AuditBenchmarkController(IBenchmarkProvider _objProvider)
        {
            objProvider = _objProvider;
        }
        /// <summary>
        /// GET: api/AuditBenchmark 
        /// Input - nothing ; 
        /// Output - List of Benchmark
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public IActionResult Get()
        {
            Dictionary<string, int> listOfProvider = new Dictionary<string, int>();
            try
            {
                listOfProvider = objProvider.GetBenchmark();
                return Ok(listOfProvider);
            }
            catch (Exception e)
            {              
                return StatusCode(500);
            }
        }
    }
}
