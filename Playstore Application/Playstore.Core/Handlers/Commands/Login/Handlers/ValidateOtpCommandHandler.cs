using MediatR;
using Microsoft.AspNetCore.Http;
 
namespace Playstore.Providers.Handlers.Commands
{
    public class ValidateOtpCommandHandler : IRequestHandler<ValidateOtpCommand, bool>
    {
        public Task<bool> Handle(ValidateOtpCommand request, CancellationToken cancellationToken)
        {
            var isOtpValid = string.Equals(request.Model.Otp, request.ResetPasswordOTP, StringComparison.OrdinalIgnoreCase);
 
            if (string.IsNullOrEmpty(request.ResetPasswordOTP))
            {
                throw new InvalidOperationException("OTP not found in command.");
            }
 
            return Task.FromResult(isOtpValid);
        }
    }
}
 