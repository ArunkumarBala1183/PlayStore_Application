using MediatR;
using Microsoft.AspNetCore.Identity;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Providers.Handlers.Queries;
 
public class ChangePasswordQueryHandler : IRequestHandler<ChangePasswordQuery,bool>
{
    private readonly IPasswordHasher<UserCredentials> passwordHasher;
    private readonly IUserCredentialsRepository userCredentialsRepository;
 
    public ChangePasswordQueryHandler(IPasswordHasher<UserCredentials> passwordHasher,IUserCredentialsRepository userCredentialsRepository)
    {
        this.passwordHasher=passwordHasher;
        this.userCredentialsRepository=userCredentialsRepository;
    }
 
    public async Task<bool> Handle(ChangePasswordQuery request, CancellationToken cancellationToken)
    {
 
        var userCredentials = await this.userCredentialsRepository.GetByIdAsync(request.userId);
        var hashedPassword = passwordHasher.VerifyHashedPassword(userCredentials, userCredentials.Password, request.password);
        if(hashedPassword==PasswordVerificationResult.Failed)
        {
            var newHashedPassword = passwordHasher.HashPassword(null,request.password);
            return await this.userCredentialsRepository.ChangePassword(request.userId,newHashedPassword);
        }
        else{
            return false;
        }
       
    }
}
 