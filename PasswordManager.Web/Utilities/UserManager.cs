using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PasswordManager.Web.Utilities
{
    public class UserManager
    {
        public Claim GetClaim(string response, string claimType)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(response);
            var claim = token.Claims.First(claim => claim.Type == claimType);
            return claim;
        }

        public async Task SignIn(HttpContext context, List<Claim> claims, string identityName)
        {
            var claimsIdentity = new ClaimsIdentity(claims, identityName);

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }
    }
}
