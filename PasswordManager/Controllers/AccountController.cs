using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Entities;
using PasswordManager.Utilities;
using System.Security.Claims;

namespace PasswordManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager _userManager;

        public AccountController(UserManager userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = identity?.FindFirst(ClaimTypes.NameIdentifier);
            var accounts = new List<UserAccount>();
            accounts.Add(new UserAccount()
            {
                DomainName = "google.com",
                UserName = "ali",
                Password = "123"
            });
            accounts.Add(new UserAccount()
            {
                DomainName = "hotmail.com",
                UserName = "ali",
                Password = "abc"
            });
            return Ok(accounts);
        }
    }
}
