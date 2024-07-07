using AndroidProjekat.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidProjekat.Validators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Email.Value).NotEmpty()
                                       .WithMessage("Email is required.")
                                       .EmailAddress()
                                       .WithMessage("Wrong email address.");

            RuleFor(x => x.Password.Value).NotEmpty()
                                          .WithMessage("Password is required.")
                                          .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$")
                                          .WithMessage("Password format is wrong. ( You need to provide one small letter, one big letter and a number, minimum length is 8 )");
        }
    }
}
