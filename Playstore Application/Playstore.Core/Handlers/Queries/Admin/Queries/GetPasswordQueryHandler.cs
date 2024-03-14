using MediatR;
using Microsoft.AspNetCore.Identity;
using Playstore.Contracts.Data;
using Playstore.Contracts.Data.Entities;
using Playstore.Contracts.Data.Repositories;
using Playstore.Providers.Handlers.Queries;
using Playstore.Providers.Handlers.Queries.Admin;

public class GetPasswordQueryHandler : IRequestHandler<GetPasswordQuery, bool>
{
    private readonly IUnitOfWork _respository;
    private readonly IPasswordHasher<UserCredentials> passwordHasher;
    private readonly IUserCredentialsRepository _credentialsRepository;
    

    public GetPasswordQueryHandler(IPasswordHasher<UserCredentials> passwordHasher, IUnitOfWork respository, IUserCredentialsRepository credentialsRepository)
    {
        this.passwordHasher = passwordHasher;
        _respository = respository;
        _credentialsRepository = credentialsRepository;
    }


    public async Task<bool> Handle(GetPasswordQuery request, CancellationToken cancellationToken)

    {
        var userCredentials = await _credentialsRepository.GetByIdAsync(request.UserId);
        var hashedPassword = passwordHasher.VerifyHashedPassword(userCredentials, userCredentials.Password, request.Password);
       
        if (hashedPassword == PasswordVerificationResult.Success)
        {
            return true;
        }
        else
        {
            // Password is incorrect
            return false;
        }
    }
}
