using MediatR;
using Microsoft.AspNetCore.Identity;
using Playstore.Contracts.Data.Repositories;
using Playstore.Providers.Handlers.Queries;

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

            string hashedPassword = passwordHasher.HashPassword(userCredentialsRepository, request.Password);
            return await userCredentialsRepository.ChangePassword(request.UserId, hashedPassword);

        }
    }
}