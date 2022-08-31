using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PasswordManager.Web.Models;
using PasswordManager.Web.Utilities;
using System.Net.Http.Headers;

namespace PasswordManager.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _apiUrl;
        private readonly HttpRequestManager _client;

        public AccountController(IConfiguration configuration, HttpRequestManager client)
        {
            _configuration = configuration;
            _apiUrl = _configuration["ApiUrl"];
            _client = client;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string token = Request.Cookies["Bearer"];
            var stringResponse = await _client.GetRequest(String.Concat(_apiUrl + "/api/Account/GetAll"), $"Bearer {token}");

            var response = JsonConvert.DeserializeObject<List<UserAccount>>(stringResponse).ToList();
            return View(response);
        }
    }
}
