using Playstore.Contracts.DTO;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Playstore.Core.Validators
{
    public class RegisterUsersDTOValidator : AbstractValidator<RegisterUsersDTO>
    {
        public RegisterUsersDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("DateofBirth is required")
                .Must(BeValidDate).WithMessage("DateofBirth must result in an age between 18 and 80");
            RuleFor(x => x.EmailId)
                .NotEmpty().WithMessage("EmailId is required")
                .EmailAddress().WithMessage("Invalid email format")
                .Must(BeValidEmail).WithMessage("Email must be a valid Gmail address");
            RuleFor(x => x.MobileNumber)
                .NotEmpty().WithMessage("MobileNumber is required")
                .Length(10).WithMessage("Mobile number should be of ten characters")
                .Must(StartsWith789).WithMessage("Mobile number should start with 7, 8, or 9");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character");
            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Password and ConfirmPassword must match");
        }

        private bool BeValidDate(DateTime date)
        {
           
            int age = CalculateAge(date);

            return age >= 18 && age <= 80;
        }

        private bool BeValidEmail(string email)
        {
            return email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase);
        }

        private bool StartsWith789(string mobileNumber)
        {
            return Regex.IsMatch(mobileNumber, "^[789]");
        }

        private int CalculateAge(DateTime birthDate)
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - birthDate.Year;
            if (currentDate.Month < birthDate.Month || (currentDate.Month == birthDate.Month && currentDate.Day < birthDate.Day))
            {
                age--;
            }

            return age;
        }
    }
}

