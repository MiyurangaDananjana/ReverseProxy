using App1.Helper;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App1.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GenToken()
        {

            // Fetch key, issuer, and audience from configuration
            var key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            // Generate JWT token
            var token = JwtTokenHelper.GenerateJwtToken(
                userId: "12345",
                key: key,
                issuer: issuer,
                audience: audience
            );

            return Ok(token);
        }

        [HttpPost]
        public IActionResult VerifyToken(string token)
        {
            // Fetch key, issuer, and audience from configuration
            var key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var claimsPrincipal = JwtTokenHelper.ValidateToken(
                token,
                 key: key,
                issuer: issuer,
                audience: audience
            );

            if (claimsPrincipal != null)
            {
                string validateMessage = "Token is valid!";
                var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Ok("User ID: " + userId + " " + validateMessage);
            }
            else
            {
                return Ok("Invalid token.");
            }
        }
    }
}
