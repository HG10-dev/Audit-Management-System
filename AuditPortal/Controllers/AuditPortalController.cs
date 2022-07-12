using AuditPortal.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuditPortal.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [Route("[Controller]")]
    [ApiController]
    public class AuditPortalController : ControllerBase
    {
        static string token;
        private IConfiguration configuration;
        public AuditPortalController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        [Route("Login")]
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthCredentials cred)
        {
            AuthCredentials input = new AuthCredentials();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(cred), Encoding.UTF8, "application/json");
                httpClient.BaseAddress = new Uri(configuration["MyLinkValue:BaseUrl"]);
                using (var response = await httpClient.PostAsync("/Auth", content))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        return Unauthorized();
                    }

                    token = await response.Content.ReadAsStringAsync();  
                    //input = JsonConvert.DeserializeObject<AuthCredentials>(token);

                    string userName = cred.UserName;
                    HttpContext.Session.SetString("token", token);
                    HttpContext.Session.SetString("user", JsonConvert.SerializeObject(cred));
                    HttpContext.Session.SetString("owner", userName);

                    return Ok();
                }
            }

        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return Ok();
            }
            HttpContext.Session.Clear();
            return Ok("Logged Out");
        }

        
        [HttpGet("Checklist/{type}")]
        public async Task<IActionResult> Get(string type)
        {
            if(type == null || (type !="Internal" && type != "SOX"))
            {
                return NotFound();
            }
            if (HttpContext.Session.GetString("token") == null)
            {
                return Unauthorized();
            }

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(configuration["MyLinkValue:BaseUrl"]);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                using (var response = await client.GetAsync("AuditChecklist/" + type))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        return StatusCode(500);
                    }

                    string list = await response.Content.ReadAsStringAsync();
                    List<Questions> qList = JsonConvert.DeserializeObject<List<Questions>>(list);
                    return Ok(qList);
                }
            }

        }

    
        [HttpPost("Severity")]
        public async Task<IActionResult> Post([FromBody] AuditRequest request)
        {
            if(request == null)
            {
                return BadRequest();
            }
            if(HttpContext.Session.GetString("token") == null)
            {
                return Unauthorized();
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(configuration["MyLinkValue:BaseUrl"]);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                StringContent content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync("AuditSeverity", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var output = await response.Content.ReadAsStringAsync();
                        AuditResponse result = JsonConvert.DeserializeObject<AuditResponse>(output);
                        return Ok(result);
                    }
                    return StatusCode(500);
                }
            }
        }
        
    }
}
