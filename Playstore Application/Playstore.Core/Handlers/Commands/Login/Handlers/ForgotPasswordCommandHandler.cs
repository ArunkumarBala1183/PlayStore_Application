using MediatR;
using Playstore.Contracts.Data.Repositories;
using Playstore.Core.Exceptions;

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
            var userCredentials = await _credentialsRepository.GetByEmailId(request.Model.EmailId);
            if (userCredentials == null)
            {
                throw new InvalidcredentialsException("Email not registered ");
            }
           
            var otp = GenerateOtp();
            _emailService.SendOtp(userCredentials.EmailId, otp);
            return otp;
        }
 
        private string GenerateOtp()
        {
            Random random = new();
            return random.Next(100000, 999999).ToString();
        }
    }
}