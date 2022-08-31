using FluentValidation;
using PasswordManager.DataAccess.Concrete;
using PasswordManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccess.ValidationRules
{
    public class AppUserValidator : AbstractValidator<AppUser>
    {
        private readonly AppDbContext _context;

        public AppUserValidator(AppDbContext context)
        {
            _context = context;

            RuleFor(u => u.UserName)
                .Must(userName => _context.AppUsers.All(u => u.UserName != userName))
                .WithMessage("UserName must be unique.");
            RuleFor(u => u.Password).NotEmpty();
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.LastName).NotEmpty();
        }
    }
}
