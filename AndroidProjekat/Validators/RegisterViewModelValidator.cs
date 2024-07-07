using AndroidProjekat.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidProjekat.Validators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Email.Value).NotEmpty().WithMessage("Email is required").EmailAddress().WithMessage("Wrong email format");

            RuleFor(x => x.Username.Value).NotEmpty().WithMessage("Username is required").Matches("^[A-z0-9-_\\s]{6,45}$").WithMessage("Wrong username format (Minimum length is 6, and it can contains letters, numbers and \"_\", \"-\")");

            RuleFor(x => x.Password.Value).NotEmpty().WithMessage("Password is required").Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,}$").WithMessage("Wrong password format ( Minimal length is 8, must contain one uppercase, lowercase letter and number )");

            RuleFor(x => x.DateOfBirth.Value).NotEmpty().WithMessage("Date of birth is required").Must(d => DateTime.UtcNow.Year - d.Year >= 10).WithMessage("You must be older than 10 years");

            RuleFor(x => x.Country.Value).NotEmpty().WithMessage("Country  is required");
        }
    }
}
