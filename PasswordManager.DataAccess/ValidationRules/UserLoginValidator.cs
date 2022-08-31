using FluentValidation;
using PasswordManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccess.ValidationRules
{
    public class UserLoginValidator : AbstractValidator<UserLogin>
    {
        public UserLoginValidator()
        {
            RuleFor(u => u.UserName).NotEmpty();
            RuleFor(u => u.UserName).NotEmpty();
        }
    }
}
