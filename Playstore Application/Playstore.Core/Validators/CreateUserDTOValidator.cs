using Playstore.Contracts.DTO;
using FluentValidation;

namespace Playstore.Core.Validators
{
    public class CreateuserDTOValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateuserDTOValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Provide passsword");
        }
    }
}
