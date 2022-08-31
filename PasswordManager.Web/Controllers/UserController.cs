using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PasswordManager.Web.Models;
using PasswordManager.Web.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _apiUrl;
        private readonly UserManager _userManager;
        private readonly HttpRequestManager _client;

        public UserController(IConfiguration configuration, UserManager userManager, HttpRequestManager client)
        {
            _configuration = configuration;
            _apiUrl = _configuration["ApiUrl"];
            _userManager = userManager;
            _client = client;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin user)
        {
            if (ModelState.IsValid)
            {
                var response = await _client.PostRequest(String.Concat(_apiUrl + "/api/User/SignIn"), user);
                if (response != null)
                {
                    Response.Cookies.Append("Bearer", response, new CookieOptions
                    {
                        Expires = DateTime.Now.AddMinutes(1)
                    });

                    Claim claim = _userManager.GetClaim(response, ClaimTypes.NameIdentifier);
                    await _userManager.SignIn(HttpContext, new List<Claim> { claim }, "Login");
                    return Json(new { success = true });
                }
            }
            return View();
        }
    }
}
