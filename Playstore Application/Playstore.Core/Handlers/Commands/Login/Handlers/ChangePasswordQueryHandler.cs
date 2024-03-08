using MediatR;
using Microsoft.AspNetCore.Identity;
using Playstore.Contracts.Data.Repositories;
using Playstore.Providers.Handlers.Queries;
 
public class ChangePasswordQueryHandler : IRequestHandler<ChangePasswordQuery,string>
{
    private readonly IPasswordHasher<object> passwordHasher;
    private readonly IUserCredentialsRepository userCredentialsRepository;
 
    public ChangePasswordQueryHandler(IPasswordHasher<object> passwordHasher,IUserCredentialsRepository userCredentialsRepository)
    {
        this.passwordHasher=passwordHasher;
        this.userCredentialsRepository=userCredentialsRepository;
    }
 
    public async Task<string> Handle(ChangePasswordQuery request, CancellationToken cancellationToken)
    {
 
        string hashedPassword=this.passwordHasher.HashPassword(null,request.password);
       return await this.userCredentialsRepository.ChangePassword(request.userId,hashedPassword);
       
    }
}
 