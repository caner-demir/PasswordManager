using PasswordManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccess.Abstract
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
    }
}
