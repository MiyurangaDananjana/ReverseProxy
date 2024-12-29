using App1.Data.Repositories.Interfaces;
using App1.Helper;
using App1.Model.DTO;
using App1.Model.Entity;
using App1.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App1.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        private readonly ILogger _logger;   

        private readonly IUserRepositories _userRepositories;

        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserRepositories userRepositories, IUserService userService)
        {
            _configuration = configuration;
            _userRepositories = userRepositories;
            _userService = userService;
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
                audience: audience,
                role : "admin"
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

        [HttpPost("login-admin")]
        public IActionResult Login(User user)
        {
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.UserName))
            {
                return BadRequest("Username and password are required.");
            }

            var  verifyUser = _userService.ValidateCredentials(user.UserName, user.Password);

            if (verifyUser)
            {
                // Fetch key, issuer, and audience from configuration
                var key = _configuration["Jwt:Key"];
                var issuer = _configuration["Jwt:Issuer"];
                var audience = _configuration["Jwt:Audience"];

                UserDTO userDetails = _userRepositories.GetUserDetailsByUsername(user.UserName);

                EncryptionHelper encryptionHelper = new EncryptionHelper();

                // Generate JWT token
                var token = JwtTokenHelper.GenerateJwtToken(
                    userId: encryptionHelper.Encrypt(userDetails.Id.ToString()),
                    key: key,
                    issuer: issuer,
                    audience: audience,
                    role : userDetails.UserRole.ToString()
                );

                // Generate token, session, or any necessary data
                return Ok(new
                {
                    Message = "Success!",
                    Token = token // Generate JWT token (hypothetical method)
                });
            }
            else
            {
                return Unauthorized("Invalid username or password.");
            }
        }
    }
}
