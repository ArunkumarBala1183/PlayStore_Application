using MediatR;
using Microsoft.AspNetCore.Http;
using Playstore.Contracts.Data.Repositories;
using Playstore.Contracts.DTO;
using System;
using System.Threading;
using System.Threading.Tasks;
 
namespace Playstore.Providers.Handlers.Commands
{
   
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, string>
    {
        private readonly IUserCredentialsRepository _credentialsRepository;
        private readonly IEmailService _emailService;
 
        public ForgotPasswordCommandHandler(IUserCredentialsRepository credentialsRepository, IEmailService emailService)
        {
            _credentialsRepository = credentialsRepository;
            _emailService = emailService;
        }
        public async Task<string> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var userCredentials = await _credentialsRepository.GetByEmailAsync(request.Model.EmailId);
            if (userCredentials == null)
            {
                return "false";
            }
           
            var otp = GenerateOtp();
            await _emailService.SendOtpAsync(userCredentials.EmailId, otp);
            return otp;
        }
 
        private string GenerateOtp()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}