using AuthorizationService.Models;
using AuthorizationService.Provider;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthorizationService.Repository
{
    public class AuthRepo : IAuthRepo
    {
        //private readonly IAuthProvider provider;
        private readonly IConfiguration config;
        public AuthRepo( IConfiguration _config)
        {
            //provider = _provider;
            config = _config;
        }
        //public override AuthCredentials Authenticate(AuthCredentials cred)
        //{
        //    if(cred == null) { return null; }

        //    AuthCredentials credentials = provider.GetAuthUser(cred);
        //    return credentials;
        //}

        public string GenerateJSONWebToken(AuthCredentials cred)
        {
            if (cred == null) { return null; }

            var authClaim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, cred.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                claims: authClaim,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
