using PasswordManager.DataAccess.Abstract;
using PasswordManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccess.Concrete
{
    public class AppUserRepository : EFRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
