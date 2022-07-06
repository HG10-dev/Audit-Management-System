using AuthorizationService.Models;
using AuthorizationService.Provider;
using AuthorizationService.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthorizationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration config;
        private readonly IAuthProvider provider;
        IAuthRepo auth_repo;

        public AuthController(IConfiguration _config, IAuthProvider _provider, IAuthRepo _repo)
        {
            config = _config;
            provider= _provider;
            auth_repo = _repo;
        }
        // POST api/<AuthController>
        [HttpPost]
        public IActionResult Login([FromBody] AuthCredentials login)
        {
            if(login == null)
            {
                return BadRequest();
            }
            

            IActionResult response = Unauthorized();
            AuthCredentials user = provider.GetAuthUser(login);
            
            if(user != null)
            {
                string tokenString = auth_repo.GenerateJSONWebToken(user);
                response = Ok(tokenString);
            }
                return response;

            //return NotFound();

        }

        
    }
}
