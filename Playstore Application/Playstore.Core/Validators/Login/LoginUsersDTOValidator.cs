// using Playstore.Contracts.DTO;
// using FluentValidation;

// namespace Playstore.Core.Validators
// {
//     public class RegisterUsersDTOValidator : AbstractValidator<RegisterUsersDTO>
//     {
//         public RegisterUsersDTOValidator()
//         {
//             RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            
//             RuleFor(x => x.DateOfBirth)
//                 .NotEmpty().WithMessage("DateofBirth is required")
//                 .Must(BeValidDate).WithMessage("DateofBirth must be between 1940 and 2006");

//             RuleFor(x => x.EmailId).NotEmpty().WithMessage("EmailId is required");
//             RuleFor(x => x.MobileNumber).NotEmpty().WithMessage("MobileNumber is required");
//             RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
//             RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("ConfirmPassword is required");
//             RuleFor(x => x.ConfirmPassword)
//                 .Equal(x => x.Password)
//                 .WithMessage("Password and ConfirmPassword must match");
//         }

//         private bool BeValidDate(DateTime date)
//         {
//             // Add your custom date range validation logic here
//             return date.Year >= 1940 && date.Year <= 2006;
//         }
//     }
// }



using Playstore.Contracts.DTO;
using FluentValidation;
using System;

namespace Playstore.Core.Validators
{
    public class LoginUsersDTOValidator : AbstractValidator<LoginUsersDTO>
    {
        public LoginUsersDTOValidator()
        {
            RuleFor(x => x.EmailId).NotEmpty().WithMessage("EmailId is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
