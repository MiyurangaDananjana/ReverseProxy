using App1.Data.Repositories.Interfaces;
using App1.Helper;
using App1.Model.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App1.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepositories _userRepositories;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserRepositories userRepositories, ILogger<UserController> logger)
        {
            _userRepositories = userRepositories;
            _logger = logger;
        }

        [HttpPost("create")]
        public IActionResult CreateUser(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_userRepositories.CheckUserNameAlreadyUse(user.UserName))
                    {
                        return BadRequest("Username already exists.");
                    }

                    user.Password = PasswordHasherHelper.HashPassword(user.Password);
                    _userRepositories.AddUser(user);
                    return Ok("New User Created Successfully!");

                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the user.");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
