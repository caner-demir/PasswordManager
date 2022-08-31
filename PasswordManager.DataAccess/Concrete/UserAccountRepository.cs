using PasswordManager.DataAccess.Abstract;
using PasswordManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccess.Concrete
{
    public class UserAccountRepository : EFRepository<UserAccount>, IUserAccountRepository
    {
        public UserAccountRepository(AppDbContext context) : base(context)
        {
        }
    }
}
