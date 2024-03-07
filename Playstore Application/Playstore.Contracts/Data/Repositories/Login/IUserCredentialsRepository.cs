using System.Linq.Expressions;
using Playstore.Contracts.Data.Entities;

namespace Playstore.Contracts.Data.Repositories
{
    public interface IUserCredentialsRepository : IRepository<UserCredentials>
    {
        //Task<UserCredentials> GetByEmailId(string emailId);
        //Task<UserCredentials> GetByConditionAsync(Expression<Func<UserCredentials, bool>> condition);
        Task<UserCredentials> GetByEmailAsync(string email);
        //Task<UserCredentials> GetByEmailWithRolesAsync(Guid id);
        Task<UserCredentials> GetByIdAsync(Guid userId);


        Task CommitAsync();
        Task<bool> Update(UserCredentials userCredentials);

    }
}