using PasswordManager.DataAccess.Abstract;
using PasswordManager.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PasswordManager.Utilities
{
    public class UserManager
    {
        private readonly IAppUserRepository _context;

        public UserManager(IAppUserRepository context)
        {
            _context = context;
        }
        public AppUser? SignIn(UserLogin user)
        {
            var currentUser = _context.GetAll(u => u.UserName == user.UserName)?[0];

            if (currentUser == null)
            {
                return null;
            }

            if (HashPassword(user.Password) == currentUser.Password)
            {
                return currentUser;
            }

            return null;
        }

        public void Register(AppUser user)
        {
            user.Password = HashPassword(user.Password);
            _context.Add(user);
        }

        private string HashPassword(string password)
        {
            var sha256 = SHA256.Create();
            var passwordBytes = Encoding.Default.GetBytes(password);
            var hashedPassword = sha256.ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashedPassword);
        }

        public Claim GetClaim(string response, string claimType)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(response);
            var claim = token.Claims.First(claim => claim.Type == claimType);
            return claim;
        }
    }
}
