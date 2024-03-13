
using Playstore.Contracts.DTO;
using FluentValidation;

namespace Playstore.Core.Validators
{
    public class PasswordResetDTOValidator : AbstractValidator<PasswordResetDTO>
    {
        public PasswordResetDTOValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Password is required")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character");
            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword)
                .WithMessage("Password and ConfirmPassword must match");
        }

        
    }
}

