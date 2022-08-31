using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PasswordManager.DataAccess.Abstract;
using PasswordManager.Entities;
using PasswordManager.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PasswordManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager _userManager;

        public UserController(UserManager userManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost("SignIn")]
        public IActionResult SignIn([FromBody] UserLogin userLogin)
        {
            var user = _userManager.SignIn(userLogin);

            if (user != null)
            {
                var token = GenerateJsonWebToken(user);
                return Ok(token);
            }

            return Unauthorized("Invalid login credentials.");
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] AppUser user)
        {
            _userManager.Register(user);
            var token = GenerateJsonWebToken(user);
            return Ok(new JsonResult(new { token = token, userName = user.UserName }));
        }

        private string GenerateJsonWebToken(AppUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
