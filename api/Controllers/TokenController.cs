using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EarthquakeApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EarthquakeApi.Controllers
{
    [ApiController]
    [Route("/api/{controller}")]
    public class TokenController : ControllerBase
    {
        private const string THE_PASSWORD = "P@ssword1";
        private readonly SecurityConfig _config;

        public TokenController(SecurityConfig config)
        {
            this._config = config;
        }

        [HttpPost]
        public ActionResult<AccessToken> RequestToken(AuthenticationRequest model)
        {
            if (model?.Password != THE_PASSWORD)
                return BadRequest("Username or password is invalid");

            var roles = new List<string> { "User" };
            if (model?.Username == "Admin")
                roles.Add("Admin");
            var token = BuildToken(model?.Username, roles);

            return Ok(token);
        }

        [HttpPost]
        [Route("singleuse")]
        public ActionResult<AccessToken> SingleUseToken(SingleUseTokenAuthenticationRequest model)
        {
            throw new NotImplementedException();
        }    

        private AccessToken BuildToken(
            string name,
            IEnumerable<string> roles)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, name));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList());

            var token = new JwtSecurityToken(
                issuer: _config.ValidIssuer,
                audience: _config.ValidAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                notBefore: DateTime.UtcNow,
                signingCredentials: _config.SigningCredentials);

            return AccessToken.WithToken(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}