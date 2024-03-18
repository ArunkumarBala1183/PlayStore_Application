using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO;
using Playstore.Core.Exceptions;

namespace Playstore.Providers.Handlers.Commands
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
    {
        private readonly IValidator<PasswordResetDTO> _validator;
        private readonly IUserCredentialsRepository _credentialsRepository;
        private readonly IPasswordHasher<UserCredentials> _passwordHasher;

        public ResetPasswordCommandHandler(IUserCredentialsRepository credentialsRepository, IValidator<PasswordResetDTO> validator, IPasswordHasher<UserCredentials> passwordHasher)
        {
            _credentialsRepository = credentialsRepository;
            _passwordHasher = passwordHasher;
            _validator = validator;
        }
        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            PasswordResetDTO validator = request.Model;

             var result = _validator.Validate(validator);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(validationMessage => validationMessage.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var emailId = request.ResetPasswordEmail;

            var userCredentials = await _credentialsRepository.GetByEmailId(emailId);

            if (userCredentials == null)
            {
                return false;
            }

            userCredentials.Password = _passwordHasher.HashPassword(userCredentials, request.Model.NewPassword); // Hash and save the new password

            await _credentialsRepository.UpdateCredentials(userCredentials);
            
            return true;
        }
    }
}