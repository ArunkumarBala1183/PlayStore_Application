
using Playstore.Contracts.DTO;
using FluentValidation;

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
