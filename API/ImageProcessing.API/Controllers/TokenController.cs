using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ImageProcessing.API.Controllers
{
    /// <summary>
    /// Token Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        /// <summary>
        /// Property Declaration
        /// </summary>
        public IConfiguration _configuration;
        
        /// <summary>
        /// Constroctor
        /// </summary>
        /// <param name="config"></param>
        public TokenController(IConfiguration config)
        {
            _configuration = config;
        }

        /// <summary>
        /// Generate Token Post Request
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post()
        {
            //create claims details based on the user information
            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWT:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["JWT:Issuer"], _configuration["JWT:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));

        }
    }
}
