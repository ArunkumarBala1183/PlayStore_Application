using MediatR;
using Microsoft.AspNetCore.Http;
 
namespace Playstore.Providers.Handlers.Commands
{
    public class ValidateOtpCommandHandler : IRequestHandler<ValidateOtpCommand, bool>
    {
        public Task<bool> Handle(ValidateOtpCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Received OTP: {request.Model.Otp}, Stored OTP: {request.ResetPasswordOTP}");
            Console.WriteLine($"Reset Password Email: {request.ResetPasswordEmail}, Reset Password OTP: {request.ResetPasswordOTP}");
 
 
            var isOtpValid = string.Equals(request.Model.Otp, request.ResetPasswordOTP, StringComparison.OrdinalIgnoreCase);
            Console.WriteLine($"Is OTP Valid: {isOtpValid}");
 
            if (string.IsNullOrEmpty(request.ResetPasswordOTP))
            {
                throw new InvalidOperationException("OTP not found in command.");
            }
 
            Console.WriteLine($"Is OTP Valid: {isOtpValid}");
 
            return Task.FromResult(isOtpValid);
        }
    }
}
 