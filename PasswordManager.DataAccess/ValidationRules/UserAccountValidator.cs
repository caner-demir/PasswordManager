using FluentValidation;
using PasswordManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccess.ValidationRules
{
    public class UserAccountValidator : AbstractValidator<UserAccount>
    {
        public UserAccountValidator()
        {
            RuleFor(a => a.UserName).NotEmpty();
            RuleFor(a => a.DomainName).NotEmpty();
            RuleFor(a => a.Password).NotEmpty();
        }
    }
}
