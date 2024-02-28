using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO;
using Playstore.Core.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Playstore.Providers.Handlers.Commands
{
public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, bool>
{
    private readonly IValidator<PasswordResetDTO> _validator;
    private readonly IUserCredentialsRepository _credentialsRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IPasswordHasher<UserCredentials> _passwordHasher;
    private readonly IMediator _mediator;

    public ResetPasswordCommandHandler(IMediator mediator,IUserCredentialsRepository credentialsRepository, IValidator<PasswordResetDTO> validator, IPasswordHasher<UserCredentials> passwordHasher, IHttpContextAccessor httpContextAccessor)
    {
        _credentialsRepository = credentialsRepository;
        _httpContextAccessor = httpContextAccessor;
        _passwordHasher = passwordHasher;
        _validator = validator;
        _mediator = mediator;
    }
public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
{
    var emailId = _httpContextAccessor.HttpContext.Session.GetString("ResetPasswordEmail");
    var otp = _httpContextAccessor.HttpContext.Session.GetString("ResetPasswordOTP");

    Console.WriteLine($"Email from session: {emailId}, OTP from session: {otp}");

    var isOtpValid = await ValidateOtp(emailId, otp);
    Console.WriteLine($"Is OTP Valid in ResetPasswordCommandHandler: {isOtpValid}");

    if (!isOtpValid || request.Model == null)
    {
        return false;
    }

    var model = new PasswordResetDTO
    {
        EmailId = emailId,
        Otp = otp,
        NewPassword = request.Model.NewPassword,
        ConfirmPassword = request.Model.ConfirmPassword
    };

    var result = _validator.Validate(model);

    if (!result.IsValid)
    {
        var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
        throw new InvalidRequestBodyException
        {
            Errors = errors
        };
    }

    var userCredentials = await _credentialsRepository.GetByEmailAsync(emailId);

    if (userCredentials == null)
    {
        return false;
    }

    userCredentials.Password = _passwordHasher.HashPassword(userCredentials, request.Model.NewPassword); // Hash and save the new password

    await _credentialsRepository.Update(userCredentials);

    return true;
}

    private async Task<bool> ValidateOtp(string emailId, string otp)
    {
        var command = new ValidateOtpCommand(new ValidateOtpDTO { EmailId = emailId, Otp = otp });
        return await _mediator.Send(command);
    }
}
}