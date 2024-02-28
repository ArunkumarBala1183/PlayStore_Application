using MediatR;
using Microsoft.AspNetCore.Http; 
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Playstore.Providers.Handlers.Commands
{
    
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, bool>
    {
        private readonly IUserCredentialsRepository _credentialsRepository;
        private readonly IEmailService _emailService; 
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ForgotPasswordCommandHandler(IUserCredentialsRepository credentialsRepository, IEmailService emailService, IHttpContextAccessor httpContextAccessor)
        {
            _credentialsRepository = credentialsRepository;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var userCredentials = await _credentialsRepository.GetByEmailAsync(request.Model.EmailId);
            if (userCredentials == null)
            {
                return false;
            }
            var otp = GenerateOtp();
            await _emailService.SendOtpAsync(userCredentials.EmailId, otp);
            _httpContextAccessor.HttpContext.Session.SetString("ResetPasswordEmail", userCredentials.EmailId);
            _httpContextAccessor.HttpContext.Session.SetString("ResetPasswordOTP", otp);
            return true;
        }

        private string GenerateOtp()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
