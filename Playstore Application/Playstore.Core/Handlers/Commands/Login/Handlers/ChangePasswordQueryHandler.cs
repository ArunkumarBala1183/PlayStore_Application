using MediatR;
using Microsoft.AspNetCore.Identity;
using Playstore.Contracts.Data.Repositories;

namespace Playstore.Providers.Handlers.Commands
{
    public class ChangePasswordQueryHandler : IRequestHandler<ChangePasswordQuery, bool>
    {
        private readonly IPasswordHasher<object> passwordHasher;
        private readonly IUserCredentialsRepository userCredentialsRepository;

        public ChangePasswordQueryHandler(IPasswordHasher<object> passwordHasher, IUserCredentialsRepository userCredentialsRepository)
        {
            this.passwordHasher = passwordHasher;
            this.userCredentialsRepository = userCredentialsRepository;
        }

         public async Task<bool> Handle(ChangePasswordQuery request, CancellationToken cancellationToken)
    {
        var userCredentials = await this.userCredentialsRepository.GetByIdAsync(request.UserId);
        var hashedPassword = passwordHasher.VerifyHashedPassword(userCredentials, userCredentials.Password, request.Password);
        if(hashedPassword==PasswordVerificationResult.Failed)
        {
            var newHashedPassword = passwordHasher.HashPassword(null,request.Password);
            return await this.userCredentialsRepository.ChangePassword(request.UserId,newHashedPassword);
        }
        else{
            return false;
        }
       
    }
    }
}