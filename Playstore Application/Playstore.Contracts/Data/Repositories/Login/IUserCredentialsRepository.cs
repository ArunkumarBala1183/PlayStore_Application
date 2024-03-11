using System.Linq.Expressions;
using Playstore.Contracts.Data.Entities;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IUserCredentialsRepository : IRepository<UserCredentials>
    {
        Task<UserCredentials> GetByEmailAsync(string email);
        Task<UserCredentials> GetByIdAsync(Guid userId);

        Task CommitAsync();
        new Task<bool> Update(UserCredentials userCredentials);
        Task<string> ChangePassword(Guid userId,string hashedPassword);

    }
}