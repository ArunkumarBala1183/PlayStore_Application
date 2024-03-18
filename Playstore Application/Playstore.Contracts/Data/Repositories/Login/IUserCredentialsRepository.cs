using System.Linq.Expressions;
using Playstore.Contracts.Data.Entities;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IUserCredentialsRepository : IRepository<UserCredentials>
    {
        Task<UserCredentials> GetByEmailId(string email);
        Task<UserCredentials> GetById(Guid userId);
        Task CommitAsync();
        Task<bool> UpdateCredentials(UserCredentials userCredentials);
        Task<bool> ChangePassword(Guid userId,string hashedPassword);

        Task<bool> checkPassword(Guid UserId, string Password);

    }
}